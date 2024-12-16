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
			return View();
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
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Products");
		}



		[HttpGet]
		public IActionResult Notification()
		{
			var notifications = _context.Notifications.ToList();
			return View(notifications);
		}
		[HttpPost]
		public IActionResult DeleteNotification(int id)
		{
			var notification = _context.Notifications.Find(id);
			if (notification != null)
			{
				_context.Notifications.Remove(notification);
				_context.SaveChanges();
			}
			return RedirectToAction("Notification");
		}

		[HttpGet]
		public IActionResult Logs(int page = 1)
		{
			int pageSize = 10; // Her sayfada kaç log gösterileceği
			int totalLogs = _context.Logs.Count(); // Toplam log sayısı
			int totalPages = (int)Math.Ceiling((totalLogs - pageSize) / (double)pageSize);

			// Son 50 logu getir
			var recentLogs = _context.Logs
				.OrderByDescending(l => l.LogDate)
				.Take(pageSize)
				.ToList();

			List<Logs> logsToShow;
			if (page == 1)
			{
				// İlk sayfa son 50 logu gösterir
				logsToShow = recentLogs;
			}
			else
			{
				// Diğer sayfalar, 50’den sonrası için sayfalama yapar
				logsToShow = _context.Logs
					.OrderByDescending(l => l.LogDate)
					.Skip(pageSize + (page - 2) * pageSize)
					.Take(pageSize)
					.ToList();
			}

			ViewBag.CurrentPage = page;
			ViewBag.TotalPages = totalPages;

			return View(logsToShow);
		}




		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Login");
		}
	}
}
