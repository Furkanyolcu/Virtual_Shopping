using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Virtual_Shopping.Models;

namespace Virtual_Shopping.Controllers
{
    public class HomeController : Controller
    {
        Context _context = new Context();
        public async Task<IActionResult> Products()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }
    }
}
