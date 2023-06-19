using BLL.Dtos;

namespace BLL.Interfaces
{
    public interface ICoinService
    {
        IEnumerable<CoinDto> Coins { get; }

        Task<CoinDto?> FindCoinAsync(CoinValue value);

        Task UpdateCoinAsync(CoinDto coinDto);

        Task TakeCoinAsync(CoinDto coinDto);
    }
}
