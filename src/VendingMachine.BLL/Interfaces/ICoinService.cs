using BLL.Dtos;

namespace BLL.Interfaces
{
    public interface ICoinService
    {
        IEnumerable<CoinDto> Coins { get; }

        Task<CoinDto?> FindCoinAsync(CoinValue value);

        Task AddCoinAsync(CoinDto coinDto);

        Task<CoinDto?> RemoveCoinAsync(CoinDto coinDto);
    }
}
