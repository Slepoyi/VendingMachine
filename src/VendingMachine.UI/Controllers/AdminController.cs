using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.UI.Extensions;
using VendingMachine.UI.Models;

namespace VendingMachine.UI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDrinkService _drinkService;
        private readonly ICoinService _coinService;

        public AdminController(IDrinkService drinkService, ICoinService coinService)
        {
            _drinkService = drinkService;
            _coinService = coinService;
        }

        [HttpGet]
        [Route("/[controller]/main")]
        public IActionResult Main()
        {
            return View();
        }

        [HttpGet]
        [Route("/[controller]/coins")]
        public IActionResult Coins()
        {
            var coins = _coinService.Coins;
            return View(coins.ToCoinViewModelEnumerable());
        }

        [HttpGet]
        [Route("/[controller]/drinks")]
        public IActionResult Drinks()
        {
            var drinks = _drinkService.Drinks;
            return View(drinks.ToDrinkViewModelEnumerable());
        }

        [HttpGet]
        [Route("/[controller]/drinks/{drinkId}")]
        public async Task<IActionResult> DrinkAsync(int drinkId)
        {
            var drink = await _drinkService.FindDrinkAsync(drinkId);
            if (drink is null)
                return NotFound("There is no drink with this id");
            return View(drink.ToDrinkViewModel());
        }

        [HttpGet]
        [Route("/[controller]/coins/{coinValue}")]
        public async Task<IActionResult> CoinAsync(CoinValue coinValue)
        {
            var coinDto = await _coinService.FindCoinAsync(coinValue);
            if (coinDto is null)
                return NotFound("There is no coin with this value");
            return View(coinDto.ToCoinViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/[controller]/editdrink")]
        public async Task EditDrink(DrinkViewModel drinkViewModel)
        {
            if (ModelState.IsValid)
            {
                await _drinkService.UpdateDrinkAsync(drinkViewModel.ToDrinkDto());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/[controller]/editcoin")]
        public async Task EditCoin(CoinViewModel coinViewModel)
        {
            if (ModelState.IsValid)
            {
                await _coinService.AddCoinAsync(coinViewModel.ToCoinDto());
            }
        }
    }
}
