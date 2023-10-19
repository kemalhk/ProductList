using Microsoft.AspNetCore.Mvc;
using ProductList.Models;
using ProductList.Services;

namespace ProductList.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProductDetails(string productId) {

            Product product = await _productService.GetProductAsync(productId);

             if (product == null)
            {
                return NotFound(); // 404 Not Found sayfasına yönlendirilebilir
            }
             return View(product);
        }
    }
}
