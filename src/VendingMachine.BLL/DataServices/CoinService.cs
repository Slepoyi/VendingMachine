using BLL.Dtos;
using BLL.Extensions;
using BLL.Interfaces;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.BLL.DataServices
{
    public class CoinService : ICoinService
    {
        private readonly IRepository<Coin, CoinValue> _coinRepository;

        public CoinService(IRepository<Coin, CoinValue> coinRepository)
        {
            _coinRepository = coinRepository;
        }

        public IEnumerable<CoinDto> Coins
            => _coinRepository.Entities.ToCoinDtoEnumerable();

        public async Task<CoinDto?> FindCoinAsync(CoinValue value)
        {
            var coin = await _coinRepository.FindAsync(value);
            if (coin is null) return null;

            return coin.ToCoinDto();
        }

        public async Task UpdateCoinAsync(CoinDto coinDto)
        {
            var existingCoin = await _coinRepository.FindAsync(coinDto.Value);
            if (existingCoin is null)
                await _coinRepository.AddAsync(coinDto.ToCoin());
            else
            {
                existingCoin.Quantity = coinDto.Quantity;
                existingCoin.IsAccepted = coinDto.IsAccepted;
                await _coinRepository.UpdateAsync(existingCoin);
            }
        }

        public async Task TakeCoinAsync(CoinDto coinDto)
        {
            var existingCoin = await _coinRepository.FindAsync(coinDto.Value);
            if (existingCoin is null)
                throw new InvalidOperationException();
            else
            {
                existingCoin.Quantity -= coinDto.Quantity;
                await _coinRepository.UpdateAsync(existingCoin);
            }
        }

        public async Task AddCoinAsync(CoinDto coinDto)
        {
            var existingCoin = await _coinRepository.FindAsync(coinDto.Value);
            if (existingCoin is null)
                throw new InvalidOperationException();
            else
            {
                existingCoin.Quantity += coinDto.Quantity;
                await _coinRepository.UpdateAsync(existingCoin);
            }
        }
    }
}
