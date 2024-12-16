using Microsoft.AspNetCore.Mvc;

namespace Virtual_Shopping.Controllers
{
    public class CartController : Controller
    {
        public IActionResult PayPage()
        {
            return View();
        }
    }
}
