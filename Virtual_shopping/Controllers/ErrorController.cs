using Microsoft.AspNetCore.Mvc;

namespace Virtual_Shopping.Controllers
{
    public class ErrorController : Controller
    { 
        // 404 - Sayfa Bulunamadı
        //[Route("Error/404")]
        public IActionResult Error404()
        {
            TempData["PreviousPage"] = Request.Headers["Referer"].ToString();
            return View();
        }

        // 500 - Sunucu Hatası
        [Route("Error/500")]
        public IActionResult Error500()
        {
            return View();
        }
    }
}
