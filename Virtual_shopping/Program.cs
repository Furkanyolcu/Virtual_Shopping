using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Virtual_Shopping.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Cookie Authentication'ý ekle
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login"; // Varsayýlan olarak öðrenci login yolu
        options.AccessDeniedPath = "/Login/Login"; // Yetkisiz eriþim için yönlendirme
    });

// Authorization ayarlarý
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomerOnly", policy => policy.RequireClaim("UserType", "Customer"));
    options.AddPolicy("SellerOnly", policy => policy.RequireClaim("UserType", "Seller"));
});



var app = builder.Build();
 
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
