using _334_group_project_web_api.DBSettings;
using _334_group_project_web_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace _334_group_project_web_api.Helpers
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _ProductCollection;

        public ProductService(
            IOptions<ProductDatabaseSettings> productDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                productDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                productDatabaseSettings.Value.DatabaseName);

            _ProductCollection = mongoDatabase.GetCollection<Product>(
                productDatabaseSettings.Value.ProductsCollectionName);
        }

        public async Task<List<Product>> GetAsync() =>
            await _ProductCollection.Find(_ => true).ToListAsync();

        public async Task<Product?> GetAsync(string id) =>
            await _ProductCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product newProduct) =>
            await _ProductCollection.InsertOneAsync(newProduct);

        public async Task UpdateAsync(string id, Product updateProduct) =>
            await _ProductCollection.ReplaceOneAsync(x => x.Id == id, updateProduct);

        public async Task RemoveAsync(string id) =>
            await _ProductCollection.DeleteOneAsync(x => x.Id == id);
    }
}
