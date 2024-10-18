using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Virtual_Shopping.Controllers
{
    public class SellerController : Controller
    {
        [Authorize(Policy = "SellerOnly")]
        public IActionResult SellerPage()
        {
            return View();
        }
        

    }
}
