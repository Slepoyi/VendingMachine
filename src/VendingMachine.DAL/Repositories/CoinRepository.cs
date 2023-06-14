using Microsoft.EntityFrameworkCore;
using VendingMachine.DAL.Context;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.DAL.Repositories
{
    public class CoinRepository : ICoinRepository
    {
        private readonly EfDbContext _efDbContext;

        public CoinRepository(EfDbContext efDbContext)
        {
            _efDbContext = efDbContext;
        }

        public IQueryable<Coin> Coins => _efDbContext.Coins;

        public async Task<Coin?> FindCoinAsync(CoinValue value)
            => await _efDbContext.Coins.FindAsync(value);

        public async Task AddCoinAsync(Coin coin)
        {
            await _efDbContext.Coins.AddAsync(coin);
            await _efDbContext.SaveChangesAsync();
        }

        public async Task RemoveCoinAsync(Coin coin)
        {
            _efDbContext.Coins.Remove(coin);
            await _efDbContext.SaveChangesAsync();
        }

        public async Task UpdateCoinAsync(Coin coin)
        {
            _efDbContext.Entry(coin).State = EntityState.Modified;
            await _efDbContext.SaveChangesAsync();
        }
    }
}
