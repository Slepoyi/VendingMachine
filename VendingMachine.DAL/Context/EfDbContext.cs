using Microsoft.EntityFrameworkCore;
using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Context
{
    public class EfDbContext : DbContext
    {
        public DbSet<Drink> Drinks { get; set; }

        public DbSet<Coin> Coins { get; set; }
    }
}
