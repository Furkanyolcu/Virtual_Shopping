﻿using Microsoft.AspNetCore.Authentication.Cookies;
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
            if (await c.Customers.AnyAsync(x => x.CustomerEmail == d.CustomerEmail))
            {
                ModelState.AddModelError("CustomerEmail", "Bu e-posta adresi zaten kullanılıyor.");
                TempData["ErrorMessage"] = "Bu e-posta adresi zaten kullaniliyor.";
                return RedirectToAction("Login", "Login");
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

            // E-posta gönder
            await _emailService.SendEmailAsync(
                d.CustomerEmail,
                "E-posta Doğrulama",
                $@"Merhaba {d.CustomerName},

        Hesabınızı etkinleştirmek için aşağıdaki bağlantıyı kullanabilirsiniz. Bu bağlantı sizi hesap doğrulama sayfasına yönlendirecektir:

        {verificationUrl}

        Eğer bağlantıya tıklayamıyorsanız, lütfen yukarıdaki URL'yi tarayıcınıza kopyalayıp yapıştırın.

        Teşekkür ederiz,
        Destek Ekibiniz"
            );

            // Kullanıcıya bilgi mesajı ile Login sayfasına yönlendir
            TempData["SuccessMessage"] = "Kaydiniz olusturuldu. Lutfen hesabinizi aktif hale getirmek icin e-postanizi kontrol edin.";
            await log.AddLog(d.CustomerEmail, "Kayıt", "Aktifleştirme linki gönderildi");
            return RedirectToAction("Login", "Login");

        }
        public async Task SignInActivation(Customer d)
        {
            // Token oluştur
            var token = new Token
            {
                Value = Guid.NewGuid().ToString(),
                ExpirationDate = DateTime.Now.AddMinutes(15), // 15 dakika
                IsActive = true, // Token geçerli
                Customer = d, // Token, bu kullanıcıya atanacak
            };

            if(await c.Tokens.AnyAsync(x => x.CustomerID == d.CustomerID))
            {
                var oldToken = await c.Tokens.FirstOrDefaultAsync(x => x.CustomerID == d.CustomerID);
                c.Tokens.Remove(oldToken);
            }
            c.Tokens.Add(token);
            await c.SaveChangesAsync();

            // Doğrulama URL'si oluştur
            var verificationUrl = Url.Action("ActivateAccount", "Login", new { token = token.Value }, Request.Scheme);

            // E-posta gönder
            await _emailService.SendEmailAsync(
                d.CustomerEmail,
                "E-posta Doğrulama",
                $@"Merhaba {d.CustomerName},

        Hesabınızı etkinleştirmek için aşağıdaki bağlantıyı kullanabilirsiniz. Bu bağlantı sizi hesap doğrulama sayfasına yönlendirecektir:

        {verificationUrl}

        Eğer bağlantıya tıklayamıyorsanız, lütfen yukarıdaki URL'yi tarayıcınıza kopyalayıp yapıştırın.

        Teşekkür ederiz,
        Destek Ekibiniz"
            );

            // Kullanıcıya bilgi mesajı ile Login sayfasına yönlendir
            await log.AddLog(d.CustomerEmail, "Kayıt", "Aktifleştirme linki yeniden gönderildi");
            TempData["SuccessMessage"] = "Kaydiniz olusturuldu. Lutfen hesabinizi aktif hale getirmek icin e-postanizi kontrol edin.";
        }


        [HttpPost]
        public async Task<IActionResult> ActivateAccount([FromBody] string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return Json(new { success = false, message = "Gecersiz istek: Token bos olamaz." });
            }

            // Token'ı ve ilişkili kullanıcıyı bul
            var userToken = await c.Tokens.Include(t => t.Customer)
                                          .FirstOrDefaultAsync(t => t.Value == token && t.IsActive);

            // Token bulunamazsa veya geçersizse
            if (userToken == null)
            {
                return Json(new { success = false });
            }


            // Kullanıcıyı aktif hale getir ve token'ı geçersiz yap
            userToken.Customer.IsActive = true;
            userToken.IsActive = false;
            userToken.ExpirationDate = DateTime.Now; // Token süresini hemen dolmuş olarak ayarla gerek olmayabiilir

            await c.SaveChangesAsync();

            // Başarılı bir şekilde hesap aktifleşti
            return Json(new { success = true, message = "Hesabınız basarıyla aktif hale getirildi!" });
        }
        





        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(Customer x)
        {
            // Müşteri bilgilerini email ve şifre ile arıyoruz
            var information = await c.Customers
                .FirstOrDefaultAsync(c => c.CustomerEmail == x.CustomerEmail && c.CustomerPassword == x.CustomerPassword);

            // Kullanıcı bulunduysa
            if (information != null)
            {
                // Eğer kullanıcı aktifse, giriş işlemini yap
                if (information.IsActive)
                {
                    await SignInUser(information.CustomerID.ToString(), information.CustomerEmail, "Customer");
                    await log.LoginLog(x.CustomerEmail, "Customer", true);
                    return RedirectToAction("Products", "Home");
                }
                else
                {
                    // Eğer kullanıcı aktif değilse, hata mesajı
                    TempData["ErrorMessage"] = "Hesabınız aktif degil. Lütfen dogrulama islemini tamamlayın.";
                    await SignInActivation(information);
                    await log.AddLog(x.CustomerEmail, "login", "Aktif olmayan hesaba giriş denemesi");
                    return RedirectToAction("Login");
                }
            }

            await log.LoginLog(x.CustomerEmail, "Seller", false);
            // Eğer kullanıcı bulunamazsa, hata mesajı
            TempData["ErrorMessage"] = "Gecersiz email veya sifre.";
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
            // Return to the login page if authentication fails
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
            else
            {
                await log.LoginLog(x.AdminEmail, "Admin", false);
                await c.SaveChangesAsync();
            }

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
                IsPersistent = true // Make the session persistent
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

    }
}