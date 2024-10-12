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
        public IActionResult SıgnIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SıgnIn(Customer x)
        {
            var information = await c.Customers.FirstOrDefaultAsync(c => c.CustomerEmail == x.CustomerEmail && c.CustomerPassword == x.CustomerPassword);

            if (information != null)
            {
                await SignInUser(information.CustomerID.ToString(), information.CustomerEmail, "SıgnIn");
                return RedirectToAction("Error404", "Error");
            }

            // Giriş başarısızsa sayfayı yenile
            return RedirectToAction("Login");
        }

        // Kullanıcıyı oturum açma işlemi
        private async Task SignInUser(string userId, string email, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Oturumu kalıcı yapmak için
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }
    }
}
