using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductList.Configuration;
using ProductList.Models;

namespace ProductList.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        
        public ProductService(IOptions<DatabaseSettings> databaseSettings) { 

            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _productCollection = mongoDb.GetCollection<Product>(databaseSettings.Value.CollectionName);
        
        }
        
        public async Task<List<Product>> GetProductsAsync() => await _productCollection.Find(_ => true).ToListAsync();

        public async Task<Product> GetProductAsync(string id) =>
            await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product product) => await _productCollection.InsertOneAsync(product);

        public async Task UpdateAsync(Product product) =>
            await _productCollection.ReplaceOneAsync(x => x.Id == product.Id, product); 

        public async Task RemoveAsync(string id) => await _productCollection.DeleteOneAsync(x => x.Id == id);
    }
}
