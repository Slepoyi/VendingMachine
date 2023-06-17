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

        public async Task AddCoinAsync(CoinDto coinDto)
        {
            var existingCoin = await _coinRepository.FindCoinAsync(coinDto.Value);
            if (existingCoin is null)
                await _coinRepository.AddCoinAsync(coinDto.ToCoin());
            else
            {
                existingCoin.Quantity += coinDto.Quantity;
                await _coinRepository.UpdateCoinAsync(existingCoin);
            }
        }

        public async Task<CoinDto?> RemoveCoinAsync(CoinDto coinDto)
        {
            var existingCoin = await _coinRepository.FindCoinAsync(coinDto.Value);
            if (existingCoin is null)
                return null;

            if (existingCoin.Quantity - coinDto.Quantity >= 0)
            {
                existingCoin.Quantity -= coinDto.Quantity;
                await _coinRepository.UpdateCoinAsync(existingCoin);
                return new CoinDto
                {
                    Value = coinDto.Value,
                    Quantity = coinDto.Quantity
                };
            }
            else
            {
                var quantityToReturn = existingCoin.Quantity;
                existingCoin.Quantity = 0;
                await _coinRepository.UpdateCoinAsync(existingCoin);
                return new CoinDto
                {
                    Value = coinDto.Value,
                    Quantity = quantityToReturn
                };
            }
        }
    }
}
