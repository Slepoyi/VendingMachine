using BLL.Dtos;
using BLL.Extensions;
using BLL.Interfaces;
using VendingMachine.DAL.Interfaces;

namespace BLL.Services
{
    public class CoinService : ICoinService
    {
        private readonly ICoinRepository _coinRepository;

        public CoinService(ICoinRepository coinRepository)
        {
            _coinRepository = coinRepository;
        }

        public IEnumerable<CoinDto> Coins
        {
            get
            {
                return _coinRepository.Coins.AsEnumerable().ToCoinDtoEnumerable();
            }
        }

        public async Task AddCoinsAsync(IEnumerable<CoinDto> coins)
        {
            foreach (var coin in coins)
            {
                var existingCoin = await _coinRepository.FindCoinAsync(coin.Value);
                if (existingCoin == null)
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
                if (existingCoin == null)
                    continue;

                if (existingCoin.Quantity - coin.Quantity >= 0)
                {
                    existingCoin.Quantity -= coin.Quantity;
                    await _coinRepository.UpdateCoinAsync(existingCoin);
                    yield return new CoinDto
                    {
                        Value = coin.Value,
                        Quantity = coin.Quantity
                    };
                }
                else
                {
                    var quantityToReturn = existingCoin.Quantity;
                    existingCoin.Quantity = 0;
                    await _coinRepository.UpdateCoinAsync(existingCoin);
                    yield return new CoinDto
                    {
                        Value = coin.Value,
                        Quantity = quantityToReturn
                    };
                }
            }
        }
    }
}
