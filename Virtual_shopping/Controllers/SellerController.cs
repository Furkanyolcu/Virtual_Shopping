using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Virtual_Shopping.Models;

namespace Virtual_Shopping.Controllers
{
    public class SellerController : Controller
    {
        Context _context = new Context();
        [Authorize(Policy = "SellerOnly")]

        [HttpGet]
        public IActionResult SellerPage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Products x)
        {
            if (ModelState.IsValid)
            {
                var newProduct = new Products
                {
                    ProductName = x.ProductName,
                    ProductDescription = x.ProductDescription,
                    ProductImageURL = x.ProductImageURL,

                };

                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction("SellerPage","Seller");


            }
            return View("Seller","Login");
        }
        

    }
}
