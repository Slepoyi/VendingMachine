using Microsoft.AspNetCore.Mvc;

namespace VendingMachine.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RemoveDrink()
        {
            return View();
        }

        public IActionResult AddDrink()
        {
            return View();
        }

        public IActionResult ChangePrice()
        {
            return View();
        }

        public IActionResult FillDrink()
        {
            return View();
        }
    }
}
