using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoalDeveloper.Models
{
    public class Kontrak
    {
        public int Id { get; set; }

        [Required]
        public string KontrakNo { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required]
        public decimal OTR { get; set; }

        [Required]
        public decimal DP { get; set; }

        [Required]
        public int JangkaWaktu { get; set; }

        [Required]
        public decimal Angsuran { get; set; }


        // Relasi
        public ICollection<JadwalAngsuran> JadwalAngsurans { get; set; }
    }
}
