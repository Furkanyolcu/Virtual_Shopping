﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Virtual_Shopping.Models;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace Virtual_Shopping.Controllers
{
	[Authorize]
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
			return RedirectToAction("Login", "Login");
		}
	}
}