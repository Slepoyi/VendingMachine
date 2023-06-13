using VendingMachine.BLL.Interfaces;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.BLL.ChangerServices
{
    public class GreedyChangerService : IChangerService
    {
        private readonly ICoinRepository _coinRepository;

        public GreedyChangerService(ICoinRepository coinRepository)
        {
            _coinRepository = coinRepository;
        }

        public async Task<Dictionary<CoinValue, int>> GetChangeAsync(int remainingMoney)
        {
            var dict = new Dictionary<CoinValue, int>
            {
                { CoinValue.Ten, 0 },
                { CoinValue.Five, 0 },
                { CoinValue.Two, 0 },
                { CoinValue.One, 0 }
            };

            foreach (var kvp in dict)
            {
                if (remainingMoney == 0)
                    break;

                var coin = await _coinRepository.FindCoinAsync(kvp.Key);
                if (coin is null || coin.Quantity == 0)
                    continue;

                var remainder = remainingMoney / (int)coin.Value;
                if (remainder == 0)
                    continue;

                if (coin.Quantity < remainder)
                    remainder = coin.Quantity;

                dict[kvp.Key] = remainder;
                remainingMoney -= remainder * (int)coin.Value;
            }
            return dict;
        }
    }
}
