using Microsoft.AspNetCore.Mvc;
using ProductList.Models;
using ProductList.Services;

namespace ProductList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;
        public HomeController(ProductService productService) => _productService = productService;
        
        public async Task<IActionResult>Index()
        {
            var productList = await _productService.GetProductsAsync();
            return View(productList);
        }
    }
}
