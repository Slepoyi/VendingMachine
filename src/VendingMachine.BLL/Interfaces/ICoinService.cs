using BLL.Dtos;

namespace BLL.Interfaces
{
    public interface ICoinService
    {
        IEnumerable<CoinDto> Coins { get; }

        Task<CoinDto?> FindCoinAsync(CoinValue value);

        Task AddCoinsAsync(IEnumerable<CoinDto> coins);

        IAsyncEnumerable<CoinDto> RemoveCoinsAsync(IEnumerable<CoinDto> coins);
    }
}
