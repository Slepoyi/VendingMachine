using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.BLL.Interfaces;
using VendingMachine.UI.Helper;
using VendingMachine.UI.Models;
using VendingMachine.UI.Options;

namespace VendingMachine.UI.Controllers
{
    public class DrinksController : Controller
    {
        private readonly IDrinkService _drinkService;
        private readonly SecretOptions _secretOptions;
        private readonly IChangerService _changerService;
        private readonly Dictionary<CoinValue, int> _coins = new()
        {
            { CoinValue.One, 0 },
            { CoinValue.Two, 0 },
            { CoinValue.Five, 0 },
            { CoinValue.Ten, 0 }
        };

        //public DrinksController(IDrinkService drinkService, IOptions<SecretOptions> secretOptions)
        //{
        //    _drinkService = drinkService;
        //    _secretOptions = secretOptions.Value;
        //}

        public IActionResult GetDrinks(string secretKey)
        {
            //if (secretKey == _secretOptions.Secret)
            //    return RedirectToAction("Main", "Admin");
            //var drinks = _drinkService.Drinks;
            var drinks = Enumerable.Empty<DrinkViewModel>();
            var dr = drinks.ToList();
            dr.Add(new DrinkViewModel
            {
                Id = 1,
                Amount = 5,
                Name = "Coffee",
                Price = 7
            });
            return View(dr);
        }

        public async Task<string> OrderDrinkAsync(int drinkId, IEnumerable<CoinViewModel> coins)
        {
            var drink = await _drinkService.FindDrinkAsync(drinkId);
            if (drink is null)
                return "Incorrect id";

            if (drink.Amount <= 0)
                return "This drink is over";

            if (GetBalance() < drink.Price)
                return "Not enough money";

            var extractTask = ExtractCoinsAsync(drink.Price);
            drink.Amount -= 1;
            var updateTask = _drinkService.UpdateDrinkAsync(drink);
            await Task.WhenAll(extractTask, updateTask);
            return $"Here is your {drink.Name}! Enjoy!";
        }

        public async Task<IEnumerable<CoinViewModel>> GetChangeAsync(int remainingMoney)
        {
            var change = await _changerService.GetChangeAsync(remainingMoney);
            return CoinDictToEnumerable.Convert(change);
        }

        public void AddCoin(CoinValue coin)
        {
            _coins[coin] += 1;
        }

        public int GetBalance()
        {
            return _coins.Sum(c => (int)c.Key * c.Value);
        }

        private async Task ExtractCoinsAsync(int value)
        {
            var coins = await _changerService.GetChangeAsync(value);
            foreach (var kvp in coins)
                _coins[kvp.Key] -= kvp.Value;
        }
    }
}