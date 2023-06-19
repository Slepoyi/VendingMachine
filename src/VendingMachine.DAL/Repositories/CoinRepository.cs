using Microsoft.EntityFrameworkCore;
using VendingMachine.DAL.Context;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.DAL.Repositories
{
    public class CoinRepository : IRepository<Coin, CoinValue>
    {
        private readonly EfDbContext _efDbContext;

        public CoinRepository(EfDbContext efDbContext)
        {
            _efDbContext = efDbContext;
        }

        public IQueryable<Coin> Entities => _efDbContext.Coins.AsNoTracking();

        public async Task<Coin?> FindAsync(CoinValue value)
            => await _efDbContext.Coins.FindAsync(value);

        public async Task AddAsync(Coin coin)
        {
            await _efDbContext.Coins.AddAsync(coin);
            await _efDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Coin coin)
        {
            _efDbContext.Coins.Remove(coin);
            await _efDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Coin coin)
        {
            //var attachedEntity = _efDbContext.ChangeTracker
            //    .Entries<Drink>()
            //    .FirstOrDefault(e => e.Entity.Id == drink.Id);

            //if (attachedEntity is not null)
            //    _efDbContext.Entry(attachedEntity.Entity).State = EntityState.Detached;

            //_efDbContext.Entry(drink).State = EntityState.Modified;

            //await _efDbContext.SaveChangesAsync();

            _efDbContext.Entry(coin).State = EntityState.Modified;
            await _efDbContext.SaveChangesAsync();
        }
    }
}
