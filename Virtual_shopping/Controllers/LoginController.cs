using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Virtual_Shopping.Models;

namespace Virtual_Shopping.Controllers
{
	public class LoginController : Controller
	{
		Context c = new Context();

		public IActionResult Login()
		{
			return View();
		}

		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(Customer d)
		{
			c.Customers.Add(d);
			await c.SaveChangesAsync();

			return RedirectToAction("Login", "Login");
		}

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(Customer x)
		{
			var information = await c.Customers
				.FirstOrDefaultAsync(c => c.CustomerEmail == x.CustomerEmail && c.CustomerPassword == x.CustomerPassword);

			if (information != null)
			{
				await SignInUser(information.CustomerID.ToString(), information.CustomerEmail, "Customer");
				return RedirectToAction("Products", "Home");
			}

			return RedirectToAction("Login");
		}

		[HttpGet]
		public IActionResult Admin()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Admin(Admin x)
		{
			var information = await c.Admins
				.FirstOrDefaultAsync(c => c.AdminEmail == x.AdminEmail && c.AdminPassword == x.AdminPassword);

			if (information != null)
			{
				await SignInUser(information.AdminID.ToString(), information.AdminEmail, "Admin");
				return RedirectToAction("Panel", "Admin");
			}

			return RedirectToAction("Panel","Admin");
		}

		private async Task SignInUser(string userId, string email, string role)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, userId),
				new Claim(ClaimTypes.Email, email),
				new Claim("UserType", role)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var authProperties = new AuthenticationProperties
			{
				IsPersistent = true // Make the session persistent
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
		}

	}
}
