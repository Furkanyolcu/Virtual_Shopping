using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Virtual_Shopping.Models;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace Virtual_Shopping.Controllers
{
	[Authorize(Policy = "AdminOnly")]
	public class AdminController : Controller
	{
		Context _context = new Context();

		public IActionResult Panel()
		{
			//var admin = _context.Admins.FirstOrDefault();	
			return View(/*admin*/);
		}

		/* PROFİL İŞLEMLERİ */
		public IActionResult Profile()
		{
			return View();
		}
        /* ÜRÜN İŞLEMLERİ */
        [HttpGet]
        public IActionResult Products()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        /* SATICI İŞLEMLERİ */
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

				_context.Sellers.Add(newSeller);
				await _context.SaveChangesAsync();

				return RedirectToAction("Sellers");


			}

			return View(model);
		}

		[HttpPost]
		public IActionResult SearchProduct(string searchTerm)
		{
			var products = _context.Products
				.Where(s => s.ProductName.Contains(searchTerm))
				.ToList();
			return View("Products", products);
		}
		[HttpPost]
		public IActionResult SearchSeller(string searchTerm)
		{
			var sellers = _context.Sellers
				.Where(s => s.SellerName.Contains(searchTerm))
				.ToList();
			return View("Sellers", sellers);
		}
        
		[HttpPost]
        public IActionResult DeleteProduct(int ID)
        {
            var product = _context.Products.Find(ID);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Products","Admin");
        }


        public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Login");
		}
	}
}
