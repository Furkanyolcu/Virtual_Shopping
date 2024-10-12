using Microsoft.AspNetCore.Mvc;

namespace Virtual_Shopping.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Products()
        {
            return View();
        }
    }
}
