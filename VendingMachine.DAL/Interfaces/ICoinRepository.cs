using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Interfaces
{
    public interface ICoinRepository
    {
        IQueryable<Coin> Coins { get; }

        Task AddCoinsAsync(IEnumerable<Coin> coins);

        Task RemoveCoinsAsync(IEnumerable<Coin> coins);
    }
}
