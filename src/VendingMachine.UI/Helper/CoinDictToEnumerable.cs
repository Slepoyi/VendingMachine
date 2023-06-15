using VendingMachine.UI.Models;

namespace VendingMachine.UI.Helper
{
    public class CoinDictToEnumerable
    {
        public static IEnumerable<CoinViewModel> Convert(Dictionary<CoinValue, int> coins)
        {
            var result = new List<CoinViewModel>();
            foreach (var kvp in coins)
            {
                result.Add(new CoinViewModel
                {
                    Value = kvp.Key,
                    Quantity = kvp.Value
                });
            }
            return result;
        }
    }
}
