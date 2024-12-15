using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Virtual_Shopping.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<ICartService, CartService>();


// Cookie Authentication'� ekle
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login"; // M��teri login yolu
        options.AccessDeniedPath = "/Login/Login"; // Yetkisiz eri�im y�nlendirmesi
    });

// Authorization ayarlar�
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomerOnly", policy => policy.RequireClaim("UserType", "Customer"));
    options.AddPolicy("SellerOnly", policy => policy.RequireClaim("UserType", "Seller"));
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("UserType", "Admin"));
});


var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
