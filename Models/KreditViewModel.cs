namespace SoalDeveloper.Models
{
    public class KreditViewModel
    {
        public decimal OTR { get; set; }           // Harga mobil
        public decimal DP { get; set; }            // Down Payment
        public int JangkaWaktu { get; set; }       // Dalam bulan
        public decimal AngsuranPerBulan { get; set; }
    }
}
