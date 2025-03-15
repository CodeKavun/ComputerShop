using Microsoft.AspNetCore.Mvc;

namespace ComputerShopApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
