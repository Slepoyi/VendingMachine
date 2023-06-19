using BLL.Dtos;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VendingMachine.BLL.Interfaces;
using VendingMachine.UI.Extensions;
using VendingMachine.UI.Models;
using VendingMachine.UI.Options;

namespace VendingMachine.UI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IDrinkService _drinkService;
        private readonly ICoinService _coinService;
        private readonly IChangerService _changerService;
        private readonly SecretOptions _secretOptions;
        private static int _balance;

        public CustomerController(IDrinkService drinkService, ICoinService coinService,
            IChangerService changerService, IOptions<SecretOptions> secretOptions)
        {
            _drinkService = drinkService;
            _coinService = coinService;
            _changerService = changerService;
            _secretOptions = secretOptions.Value;
        }

        [HttpGet]
        public IActionResult Main(string secretKey)
        {
            if (secretKey == _secretOptions.Secret)
                return RedirectToAction("main", "admin");
            _balance = 0;
            var coins = _coinService.Coins;
            ViewData["Coins"] = coins;
            var drinks = _drinkService.Drinks;
            return View(drinks.ToDrinkViewModelEnumerable());
        }

        [HttpGet]
        public IActionResult DrinksPartial()
        {
            ViewBag.Balance = _balance;
            var drinks = _drinkService.Drinks;
            return PartialView(drinks.ToDrinkViewModelEnumerable());
        }

        [HttpPost]
        public async Task<IActionResult> OrderDrinkAsync(int drinkId)
        {
            var drink = await _drinkService.FindDrinkAsync(drinkId);
            if (drink is null)
                return NotFound("Incorrect id");

            if (drink.Amount <= 0)
                return BadRequest("This drink is over");

            if (_balance < drink.Price)
                return BadRequest("Not enough money");

            _balance -= drink.Price;
            drink.Amount -= 1;
            await _drinkService.UpdateDrinkAsync(drink);
            
            return Ok($"Here is your {drink.Name}! Enjoy!");
        }

        [HttpPost]
        public async Task<IEnumerable<CoinViewModel>> GetChangeAsync()
        {
            var change = await _changerService.GetChangeAsync(_balance);
            foreach (var coin in change)
            {
                if (coin.Quantity == 0) continue;
                await _coinService.TakeCoinAsync(coin);
            }
            _balance = 0;
            return change.ToCoinViewModelEnumerable();
        }

        [HttpPost]
        public async Task AddCoinAsync(CoinValue value)
        {
            var coinDto = await _coinService.FindCoinAsync(value);
            if (coinDto is null || !coinDto.IsAccepted) return;

            _balance += (int)value;

            await _coinService.UpdateCoinAsync(
                new CoinDto
                {
                    Value = value,
                    IsAccepted = true,
                    Quantity = 1
                });
        }

        [HttpGet]
        public int GetBalance() => _balance;
    }
}