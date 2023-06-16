using BLL.Dtos;
using BLL.Extensions;
using BLL.Interfaces;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.BLL.DataServices
{
    public class CoinService : ICoinService
    {
        private readonly ICoinRepository _coinRepository;

        public CoinService(ICoinRepository coinRepository)
        {
            _coinRepository = coinRepository;
        }

        public IEnumerable<CoinDto> Coins
            => _coinRepository.Coins.AsEnumerable().ToCoinDtoEnumerable();

        public async Task<CoinDto?> FindCoinAsync(CoinValue value)
        {
            var coin = await _coinRepository.FindCoinAsync(value);
            if (coin is null)
                return null;

            return coin.ToCoinDto();
        }

        public async Task AddCoinsAsync(IEnumerable<CoinDto> coins)
        {
            foreach (var coin in coins)
            {
                var existingCoin = await _coinRepository.FindCoinAsync(coin.Value);
                if (existingCoin is null)
                    await _coinRepository.AddCoinAsync(coin.ToCoin());
                else
                {
                    existingCoin.Quantity += coin.Quantity;
                    await _coinRepository.UpdateCoinAsync(existingCoin);
                }
            }
        }

        public async IAsyncEnumerable<CoinDto> RemoveCoinsAsync(IEnumerable<CoinDto> coins)
        {
            foreach (var coin in coins)
            {
                var existingCoin = await _coinRepository.FindCoinAsync(coin.Value);
                if (existingCoin is null)
                    continue;

                if (existingCoin.Quantity - coin.Quantity >= 0)
                {
                    existingCoin.Quantity -= coin.Quantity;
                    var updTask = _coinRepository.UpdateCoinAsync(existingCoin);
                    yield return new CoinDto
                    {
                        Value = coin.Value,
                        Quantity = coin.Quantity
                    };
                    await updTask;
                }
                else
                {
                    var quantityToReturn = existingCoin.Quantity;
                    existingCoin.Quantity = 0;
                    var updTask = _coinRepository.UpdateCoinAsync(existingCoin);
                    yield return new CoinDto
                    {
                        Value = coin.Value,
                        Quantity = quantityToReturn
                    };
                    await updTask;
                }
            }
        }
    }
}
