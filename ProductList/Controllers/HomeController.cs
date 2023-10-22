using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ProductList.Models;
using ProductList.Services;
using System.Text.Json;

namespace ProductList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(ProductService productService, IHttpClientFactory clientFactory)
        {
            _productService = productService;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await _productService.GetProductsAsync();
            //eğer ürün yoksa otomatik olarak ürün ekleme işlemi 
            if (productList == null)
            {
                //get isteği oluştur
                var request = new HttpRequestMessage(HttpMethod.Get,
                            "https://fakestoreapi.com/products");
                // Client oluşturma
                var client = _clientFactory.CreateClient();
                //url e istek at
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    //gelen datayı fakeproduct nesnesine dönüştür
                    var fakeProducts = await JsonSerializer.DeserializeAsync
                        <IEnumerable<FakeProduct>>(responseStream);
                    foreach (var item in fakeProducts)
                    {
                        Product product = new Product();
                        product.Name = item.title;
                        product.Description = item.description;
                        product.Price = item.price;
                        product.ImageURL = item.image;

                        await _productService.CreateAsync(product);

                    }
                }
            }

            return View(productList);
        }

        [HttpGet]
        public IActionResult ProductAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductAdd(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateAsync(product);
                return RedirectToAction("Index");
            }
            //hatalı ise 
            return View(product);
        }


        
        public async Task<IActionResult> Cart()
        {
            return View();
        }



    }
}
