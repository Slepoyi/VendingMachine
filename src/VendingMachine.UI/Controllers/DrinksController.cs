using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VendingMachine.UI.Models;
using VendingMachine.UI.Options;

namespace VendingMachine.UI.Controllers
{
    public class DrinksController : Controller
    {
        private readonly IDrinkService _drinkService;
        private readonly SecretOptions _secretOptions;

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
            ViewBag.Balance = 0;
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

        public void OrderDrink(int drinkId, IEnumerable<CoinViewModel> coins)
        {

        }

        public void GetChange(int remainingMoney)
        {

        }
    }
}