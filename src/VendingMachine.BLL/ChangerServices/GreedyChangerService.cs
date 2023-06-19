using BLL.Dtos;
using VendingMachine.BLL.Interfaces;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.BLL.ChangerServices
{
    public class GreedyChangerService : IChangerService
    {
        private readonly IRepository<Coin, CoinValue> _coinRepository;

        public GreedyChangerService(IRepository<Coin, CoinValue> coinRepository)
        {
            _coinRepository = coinRepository;
        }

        public async Task<IEnumerable<CoinDto>> GetChangeAsync(int remainingMoney)
        {
            var coinDtos = new List<CoinDto>()
            {
                new CoinDto
                {
                    Value = CoinValue.Ten,
                    Quantity = 0
                },
                new CoinDto
                {
                    Value = CoinValue.Five,
                    Quantity = 0
                },
                new CoinDto
                {
                    Value = CoinValue.Two,
                    Quantity = 0
                },
                new CoinDto
                {
                    Value = CoinValue.One,
                    Quantity = 0
                },
            };

            foreach (var coinDto in coinDtos)
            {
                if (remainingMoney == 0)
                    break;

                var coin = await _coinRepository.FindAsync(coinDto.Value);
                if (coin is null || coin.Quantity == 0)
                    continue;

                var remainder = remainingMoney / (int)coin.Value;
                if (remainder == 0)
                    continue;

                if (coin.Quantity < remainder)
                    remainder = coin.Quantity;

                coinDto.Quantity = remainder;
                remainingMoney -= remainder * (int)coin.Value;
            }
            return coinDtos;
        }
    }
}
