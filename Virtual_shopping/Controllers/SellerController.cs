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
        public async Task<IActionResult> SellerPageAsync()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
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
                    ProductPrice = x.ProductPrice
                };

                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction("Seller",x);


            }
            return View("Seller","Login");
        }
        

    }
}
