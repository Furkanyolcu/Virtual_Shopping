using Microsoft.AspNetCore.Mvc;

namespace Virtual_Shopping.Controllers
{
    public class SellerController : Controller
    {
        public IActionResult SellerPage()
        {
            return View();
        }
        public IActionResult SellerLogin()
        {
            return View();
        }

    }
}
