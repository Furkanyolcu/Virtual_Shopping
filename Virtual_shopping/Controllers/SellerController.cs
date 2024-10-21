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
        public async Task<IActionResult> AddProduct(string ProductName, string ProductDescription, string ProductImageURL, int ProductPrice)
        {
            var newProduct = new Products
            {
                ProductName = ProductName,
                ProductDescription = ProductDescription,
                ProductImageURL = ProductImageURL,
                ProductPrice = ProductPrice
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("SellerPage");
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
            return RedirectToAction("SellerPage");
        }

        [HttpPost]
        public async Task<IActionResult> updateProduct(int id, string ProductName, string ProductDescription, string ProductImageURL, int ProductPrice)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.ProductName = ProductName;
                product.ProductDescription = ProductDescription;
                product.ProductImageURL = ProductImageURL;
                product.ProductPrice = ProductPrice;

                await _context.SaveChangesAsync();

                return RedirectToAction("SellerPage");
            }
            return View("SellerPage");
        }



    }
}
