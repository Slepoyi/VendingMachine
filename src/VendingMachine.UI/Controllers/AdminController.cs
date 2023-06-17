using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.UI.Extensions;

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
        public IActionResult Main()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Coins()
        {
            var coins = _coinService.Coins;
            return View(coins.ToCoinViewModelEnumerable());
        }

        [HttpGet]
        public IActionResult Drinks()
        {
            var drinks = _drinkService.Drinks;
            return View(drinks.ToDrinkViewModelEnumerable());
        }

        [HttpGet]
        public IActionResult Drink()
        {
            var coins = _coinService.Coins;
            return View(coins.ToCoinViewModelEnumerable());
        }

        [HttpGet]
        public IActionResult Coin()
        {
            var coins = _coinService.Coins;
            return View(coins.ToCoinViewModelEnumerable());
        }

        [HttpPost]
        public async Task EditDrink()
        {
            if (ModelState.IsValid)
            {

            }
        }

        [HttpPost]
        public async Task EditCoin()
        {
            if (ModelState.IsValid)
            {

            }
        }
    }
}
