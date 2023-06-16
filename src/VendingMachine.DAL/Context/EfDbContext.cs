using Microsoft.EntityFrameworkCore;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Seed;

namespace VendingMachine.DAL.Context
{
    public class EfDbContext : DbContext
    {
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Coin> Coins { get; set; }

        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drink>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Drink>()
                .HasKey(d => d.Id);
            modelBuilder.Entity<Coin>()
                .HasKey(c => c.Value);

            modelBuilder.Entity<Coin>()
                .HasData(SeedData.Coins);
            modelBuilder.Entity<Drink>()
                .HasData(SeedData.Drinks);

            base.OnModelCreating(modelBuilder);
        }
    }
}
