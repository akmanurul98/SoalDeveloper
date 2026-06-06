using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoalDeveloper.Models
{
    [Table("JadwalAngsuran")]
    public class JadwalAngsuran
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string KontrakNo { get; set; } = string.Empty;

        [Required]
        public int AngsuranKe { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AngsuranPerBulan { get; set; }

        [Required]
        public DateTime TanggalJatuhTempo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SisaTagihan { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalBayar { get; set; }   // total yang terakhir dibayar

        public DateTime? TanggalPembayaran { get; set; }   // tanggal bayar terakhir

        [StringLength(20)]
        public string StatusPembayaran { get; set; } = "Belum Lunas";

        // Relasi (opsional, kalau ada model Kontrak)
        // public string? KontrakId { get; set; }
        // public Kontrak? Kontrak { get; set; }
    }
}
