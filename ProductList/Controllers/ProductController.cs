using Microsoft.AspNetCore.Mvc;
using ProductList.Models;
using ProductList.Services;

namespace ProductList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService) => _productService = productService;

        
        public async Task<ActionResult> Get(string id) { 
            var existingProduct = await _productService.GetProductAsync(id);
            if (existingProduct is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(existingProduct);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var allProducts = await _productService.GetProductsAsync();

            if (allProducts == null || !allProducts.Any())
            {
                return NotFound();
            }
            return Ok(allProducts);
        }
    }
}
