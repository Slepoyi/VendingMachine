using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Interfaces
{
    public interface ICoinRepository
    {
        IQueryable<Coin> Coins { get; }

        Task<Coin?> FindCoinAsync(CoinValue value);

        Task AddCoinAsync(Coin coin);

        Task RemoveCoinAsync(Coin coin);

        Task UpdateCoinAsync(Coin coin);
    }
}
