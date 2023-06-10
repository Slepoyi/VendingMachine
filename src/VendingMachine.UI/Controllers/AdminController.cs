using Microsoft.AspNetCore.Mvc;

namespace VendingMachine.UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
