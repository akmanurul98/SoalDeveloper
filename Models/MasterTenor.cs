using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoalDeveloper.Models
{
    [Table("MasterTenor")]
    public class MasterTenor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Jangka Waktu (Bulan)")]
        public int JangkaWaktu { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,4)")]
        [Display(Name = "Bunga")]
        public decimal Bunga { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}