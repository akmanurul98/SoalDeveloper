using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoalDeveloper.Models
{
    [Table("Pembayaran")]
    public class Pembayaran
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string KontrakNo { get; set; }

        public int AngsuranKe { get; set; }

        [Column(TypeName = "date")]
        public DateTime TanggalBayar { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalBayar { get; set; }
    }
}
