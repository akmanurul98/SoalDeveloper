using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoalDeveloper.Models;

public class KontrakController : Controller
{
    private readonly FinanceContext _context;

    public KontrakController(FinanceContext context)
    {
        _context = context;
    }
    public class BayarDto
    {
        public string KontrakNo { get; set; } = string.Empty;
        public int AngsuranKe { get; set; }
        public decimal TotalBayar { get; set; }
        public DateTime TanggalBayar { get; set; }
    }

    // GET: /Kontrak/Index
    public IActionResult Index()
    {
        var kontraks = _context.Kontrak
            .Include(k => k.JadwalAngsurans)
            .ToList();
        return View(kontraks);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(string kontrakNo, string clientName, decimal otr, int dp_percen, int jangkaWaktu)
    {
        decimal dp = dp_percen * otr / 100;
        decimal pokokUtang = otr - dp;
        decimal bunga = (jangkaWaktu <= 12) ? 0.12m :
                        (jangkaWaktu <= 24) ? 0.14m : 0.165m;

        decimal angsuran = (pokokUtang + (pokokUtang * bunga)) / jangkaWaktu;

        var kontrak = new Kontrak
        {
            KontrakNo = kontrakNo,
            ClientName = clientName,
            OTR = otr,
            DP = dp,
            JangkaWaktu = jangkaWaktu,
            Angsuran = angsuran,
        };
        _context.Kontrak.Add(kontrak);

        // 📌 Tentukan tanggal mulai angsuran
        DateTime startDate = new DateTime(2024, 1, 25); // Angsuran ke-1

        for (int i = 1; i <= jangkaWaktu; i++)
        {
            var jadwal = new JadwalAngsuran
            {
                KontrakNo = kontrakNo,
                AngsuranKe = i,
                AngsuranPerBulan = Math.Round(angsuran, 0),
                // 📌 i-1 supaya angsuran ke-1 = startDate
                TanggalJatuhTempo = startDate.AddMonths(i - 1),
                StatusPembayaran = "Belum Lunas",
                TotalBayar = 0,
                SisaTagihan = Math.Round(angsuran, 0),
            };
            _context.JadwalAngsuran.Add(jadwal);
        }

        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Detail(string id)
    {
        if (id == null) return NotFound();

        var kontrak = _context.Kontrak
            .Include(k => k.JadwalAngsurans)
            .FirstOrDefault(k => k.KontrakNo == id);

        if (kontrak == null) return NotFound();

        return View(kontrak);
    }

    [HttpPost]
    public async Task<IActionResult> Bayar([FromBody] BayarDto data)
    {
        if (data == null)
            return BadRequest("Data pembayaran tidak valid.");

        var jadwal = await _context.JadwalAngsuran
            .FirstOrDefaultAsync(x => x.KontrakNo == data.KontrakNo && x.AngsuranKe == data.AngsuranKe);

        if (jadwal == null)
            return NotFound();

        // Update pembayaran
        jadwal.SisaTagihan -= data.TotalBayar;

        if (jadwal.SisaTagihan <= 0)
        {
            jadwal.SisaTagihan = 0;
            jadwal.StatusPembayaran = "Lunas";
        }
        else if (jadwal.SisaTagihan > 0)
        {
            jadwal.StatusPembayaran = "Bayar Sebagian";
        }
        else
        {
            jadwal.StatusPembayaran = "Belum Lunas";
        }

            jadwal.TotalBayar = data.TotalBayar;
        jadwal.TanggalPembayaran = data.TanggalBayar;

        _context.JadwalAngsuran.Update(jadwal);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            status = jadwal.StatusPembayaran,
            sisa = jadwal.SisaTagihan,
            tanggal = jadwal.TanggalPembayaran?.ToString("yyyy-MM-dd")
        });
    }




}
