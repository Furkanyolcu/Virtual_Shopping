using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Virtual_Shopping.Models;
using System.ComponentModel;

namespace Virtual_Shopping.Controllers
{
	public class AdminController : Controller
	{
		private readonly Context _context;

		public AdminController(Context context)
		{
			_context = context;
		}

		public IActionResult Panel()
		{
			return View();
		}

		/* PROFİL İŞLEMLERİ */
		public IActionResult Profile()
		{
			return View();
		}

		/* ÖĞRENCİ İŞLEMLERİ */
		[HttpGet]
		public IActionResult Sellers()
		{
			var seller = _context.Sellers.ToList();
			return View(seller);
		}
		[HttpGet]
		public IActionResult AddSeller()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> AddSeller(AddSellerView model)
		{
			if (ModelState.IsValid)
			{
				var newSeller = new Seller
				{
					SellerName = model.SellerName,
					SellerEmail = model.SellerEmail,
					SellerPassword = model.SellerPassword
				};
			}

			return View(model);
		}

        [HttpPost]
        public IActionResult SearchSeller(string searchTerm)
        {
            var sellers = _context.Sellers
                .Where(s => s.SellerName.Contains(searchTerm))
                .ToList();
            return View("Sellers", sellers);
        }


        public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Home", "Login");
		}
	}
}
