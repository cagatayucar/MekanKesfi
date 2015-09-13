namespace MekanKesfi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MekanKesfiModel : DbContext
    {
        public MekanKesfiModel()
            : base("name=MekanKesfiModel")
        {
        }

        public virtual DbSet<FirmaLogin> FirmaLogin { get; set; }
        public virtual DbSet<Mekanlar> Mekanlar { get; set; }
        public virtual DbSet<Reklam> Reklam { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mekanlar>()
                .Property(e => e.telefon)
                .IsFixedLength();

            modelBuilder.Entity<Mekanlar>()
                .Property(e => e.latitude)
                .HasPrecision(25, 20);

            modelBuilder.Entity<Mekanlar>()
                .Property(e => e.longitude)
                .HasPrecision(25, 20);

            modelBuilder.Entity<Mekanlar>()
                .HasMany(e => e.Reklam)
                .WithOptional(e => e.Mekanlar)
                .HasForeignKey(e => e.mekan_id);
        }
    }
}
