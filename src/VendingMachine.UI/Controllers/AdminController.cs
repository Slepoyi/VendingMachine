using BLL.Dtos;
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
        public IActionResult DrinksPartial()
        {
            var drinks = _drinkService.Drinks;
            return PartialView(drinks.ToDrinkViewModelEnumerable());
        }

        [HttpGet]
        public async Task<IActionResult> DrinkAsync(int drinkId)
        {
            if (drinkId == 0)
                return View(new DrinkDto().ToDrinkViewModel());

            var drink = await _drinkService.FindDrinkAsync(drinkId);
            if (drink is null)
                return NotFound("There is no drink with this id");

            return View(drink.ToDrinkEditModel());
        }

        [HttpGet]
        public async Task<IActionResult> CoinAsync(CoinValue coinValue)
        {
            var coinDto = await _coinService.FindCoinAsync(coinValue);
            if (coinDto is null)
                return NotFound("There is no coin with this value");
            return View(coinDto.ToCoinViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDrinkAsync(DrinkEditModel drinkEditModel)
        {
            if (ModelState.IsValid)
                await _drinkService.UpdateDrinkAsync(drinkEditModel.ToDrinkDto());

            return RedirectToAction("Drinks", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCoinAsync(CoinViewModel coinViewModel)
        {
            if (ModelState.IsValid)
                await _coinService.UpdateCoinAsync(coinViewModel.ToCoinDto());

            return RedirectToAction("Coins", "Admin");
        }

        [HttpPost]
        public async Task DeleteDrinkAsync(int drinkId)
        {
            await _drinkService.DeleteDrinkAsync(drinkId);
        }
    }
}
