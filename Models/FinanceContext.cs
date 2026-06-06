using Microsoft.EntityFrameworkCore;

namespace SoalDeveloper.Models
{
    public class FinanceContext : DbContext
    {
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options)
        {
        }

        public DbSet<Kontrak> Kontrak { get; set; }
        public DbSet<JadwalAngsuran> JadwalAngsuran { get; set; }
        public DbSet<Pembayaran> Pembayarans { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kontrak>()
                .HasMany(k => k.JadwalAngsurans)
                .WithOne()
                .HasForeignKey(j => j.KontrakNo)
                .HasPrincipalKey(k => k.KontrakNo);
        }
    }
}
