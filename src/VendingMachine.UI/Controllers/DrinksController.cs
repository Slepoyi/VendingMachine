using BLL.Dtos;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.BLL.Interfaces;
using VendingMachine.UI.Extensions;
using VendingMachine.UI.Models;
using VendingMachine.UI.Options;

namespace VendingMachine.UI.Controllers
{
    public class DrinksController : Controller
    {
        private readonly IDrinkService _drinkService;
        private readonly ICoinService _coinService;
        private readonly SecretOptions _secretOptions;
        private readonly IChangerService _changerService;
        private static int _balance;

        public DrinksController(IDrinkService drinkService, ICoinService coinService,
            SecretOptions secretOptions, IChangerService changerService)
        {
            _drinkService = drinkService;
            _coinService = coinService;
            _secretOptions = secretOptions;
            _changerService = changerService;
            ViewBag.Balance = 0;
            _balance = 0;
        }

        [HttpGet]
        public IActionResult GetDrinks(string secretKey)
        {
            if (secretKey == _secretOptions.Secret)
                return RedirectToAction("Main", "Admin");
            var drinks = _drinkService.Drinks;
            return View(drinks);
        }

        [HttpGet]
        public IActionResult GetDrinksPartial()
        {
            var drinks = _drinkService.Drinks;
            return PartialView(drinks);
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

        [HttpGet]
        public async Task<IEnumerable<CoinViewModel>> GetChangeAsync()
        {
            var change = await _changerService.GetChangeAsync(_balance);
            _balance = 0;
            return change.ToCoinViewModelEnumerable();
        }

        [HttpPost]
        public async Task AddCoinAsync(CoinValue value)
        {
            var coinDto = await _coinService.FindCoinAsync(value);

            if (coinDto is null || !coinDto.IsAccepted)
                return;

            _balance += (int)value;

            await _coinService.AddCoinsAsync(
                new CoinDto[]
                {
                    new CoinDto
                    {
                        Value = value,
                        IsAccepted = true,
                        Quantity = 1
                    }
                });
        }

        [HttpGet]
        public int GetBalance()
        {
            ViewBag.Balance = _balance;
            return _balance;
        }
    }
}