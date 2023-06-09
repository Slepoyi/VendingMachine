using Microsoft.EntityFrameworkCore;
using VendingMachine.DAL.Context;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.DAL.Services
{
    public class CoinRepository : ICoinRepository
    {
        private readonly EfDbContext _efDbContext;

        public CoinRepository(EfDbContext efDbContext)
        {
            _efDbContext = efDbContext;
        }

        public IQueryable<Coin> Coins => _efDbContext.Coins;

        public async Task AddCoinsAsync(IEnumerable<Coin> coins)
        {
            foreach (var coin in coins)
            {
                var existingCoin = await _efDbContext.Coins.FindAsync(coin.Value);
                if (existingCoin == null)
                    _efDbContext.Coins.Add(coin);
                else
                    existingCoin.Quantity += coin.Quantity;
            }
            await _efDbContext.SaveChangesAsync();
        }

        public async Task RemoveCoinsAsync(IEnumerable<Coin> coins)
        {
            foreach (var coin in coins)
            {
                var existingCoin = await _efDbContext.Coins.FindAsync(coin.Value);
                if (existingCoin == null)
                    continue;
                else if (CoinShouldBeDeleted(coin, existingCoin))
                    _efDbContext.Coins.Remove(existingCoin);
                else
                    existingCoin.Quantity -= coin.Quantity;
            }
            await _efDbContext.SaveChangesAsync();
        }

        private static bool CoinShouldBeDeleted(Coin coinToDelete, Coin existingCoin)
        {
            if (coinToDelete.Value != existingCoin.Value)
                return false;

            if (existingCoin.Quantity > coinToDelete.Quantity)
                return false;

            return true;
        }
    }
}
