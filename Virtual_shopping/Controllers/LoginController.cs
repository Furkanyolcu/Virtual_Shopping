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
		LogController log = new LogController();

		private readonly EmailService _emailService;

		public LoginController(EmailService emailService)
		{
			_emailService = emailService;
		}
		public IActionResult Login()

		{
			return View();
		}

		[HttpGet]
		public IActionResult SignUp()
		{
			return View();

		}
		[HttpGet]
		public IActionResult ActivateAccount()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(Customer d)
		{
			var customer= await c.Customers.FirstOrDefaultAsync(x => x.CustomerEmail == d.CustomerEmail);
			if (customer != null)
			{
				//ModelState.AddModelError("CustomerEmail", "Bu e-posta adresi zaten kullanılıyor.");
				if (customer.IsActive)
				{
					TempData["ErrorMessage"] = "Bu mail zaten kayıtlı";
					return RedirectToAction("Login", "Login");
				}
				else
				{
					TempData["ErrorMessage"] = "Bu mail aktif değil, oany maili tekrardan gönderildi";
				}
				
			}

			// Kullanıcıyı pasif olarak kaydet
			d.IsActive = false;

			// Token oluştur
			var token = new Token
			{
				Value = Guid.NewGuid().ToString(),
				ExpirationDate = DateTime.Now.AddMinutes(15), // 15 dakika
				IsActive = true, // Token geçerli
				Customer = d // Token, bu kullanıcıya atanacak
			};

			c.Customers.Add(d);
			c.Tokens.Add(token);
			await c.SaveChangesAsync();

			// Doğrulama URL'si oluştur
			var verificationUrl = Url.Action("ActivateAccount", "Login", new { token = token.Value }, Request.Scheme);

			// E-posta gönder (Token sadece metin olarak gönderilecek)
			await _emailService.SendEmailAsync(
				d.CustomerEmail,
				"E-posta Doğrulama",
				$"Merhaba {d.CustomerName}, lütfen hesabınızı doğrulamak için aşağıdaki bağlantıya tıklayın veya URL'yi tarayıcınıza yapıştırın: {verificationUrl}"
			);

			// Kullanıcıyı Login sayfasına yönlendir
			return RedirectToAction("Login", "Login");
		}

		[HttpPost]
		public async Task<IActionResult> ActivateAccount([FromBody] string token)
		{
			if (string.IsNullOrWhiteSpace(token))
			{
				return Json(new { success = false, message = "Geçersiz istek: Token boş olamaz." });
			}

			// Token'ı ve ilişkili kullanıcıyı bul
			var userToken = await c.Tokens.Include(t => t.Customer)
										  .FirstOrDefaultAsync(t => t.Value == token && t.IsActive);

			// Token bulunamazsa veya geçersizse
			if (userToken == null)
			{
				return Json(new { success = false, message = "Token geçersiz veya süresi dolmuş." });
			}

			// Kullanıcı zaten aktifse
			if (userToken.Customer.IsActive)
			{
				return Json(new { success = false, message = "Hesabınız zaten aktif durumda." });
			}

			// Kullanıcıyı aktif hale getir ve token'ı geçersiz yap
			userToken.Customer.IsActive = true;
			userToken.IsActive = false;
			userToken.ExpirationDate = DateTime.Now; // Token süresini hemen dolmuş olarak ayarla gerek olmayabiilir

			await c.SaveChangesAsync();

			// Başarılı bir şekilde hesap aktifleşti
			return Json(new { success = true, message = "Hesabınız başarıyla aktif hale getirildi!" });
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
				if (information.IsActive)
				{
					await SignInUser(information.CustomerID.ToString(), information.CustomerEmail, "Customer");
					await log.LoginLog(x.CustomerEmail, "Customer", true);
					return RedirectToAction("Products", "Home");
				}
				else
				{
					TempData["ErrorMessage"] = "Hesabınız aktif değil. Lütfen doğrulama işlemini tamamlayın.";
					await log.LoginLog(x.CustomerEmail, "Seller", false);
					return RedirectToAction("Login");
				}
			}
			await log.LoginLog(x.CustomerEmail, "Customer", false);
			TempData["ErrorMessage"] = "Geçersiz email veya şifre.";
			return RedirectToAction("Login");
		}


		[HttpGet]
		public IActionResult SellerLogin()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SellerLogin(Seller x)
		{
			var information = await c.Sellers
				.FirstOrDefaultAsync(c => c.SellerEmail == x.SellerEmail && c.SellerPassword == x.SellerPassword);

			if (information != null)
			{
				await SignInUser(information.SellerID.ToString(), information.SellerEmail, "Seller");
				await log.LoginLog(x.SellerEmail, "Seller", true);
				return RedirectToAction("SellerPage", "Seller");
			}
			await log.LoginLog(x.SellerEmail, "Seller", false);
			TempData["ErrorMessage"] = "Gecersiz email veya sifre.";
			return RedirectToAction("SellerLogin", "Login");
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
				await log.LoginLog(x.AdminEmail, "Admin", true);
				return RedirectToAction("Panel", "Admin");
			}
			await log.LoginLog(x.AdminEmail, "Admin", false);
			TempData["ErrorMessage"] = "Gecersiz email veya sifre.";
			return RedirectToAction("Admin", "Login");
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
				IsPersistent = true
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
		}

	}
}
