using _334_group_project_web_api.DBSettings;
using _334_group_project_web_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace _334_group_project_web_api.Helpers
{
    public class ShoppingListService
    {
        private readonly IMongoCollection<ShoppingList> _ShoppingListCollection;

        public ShoppingListService(
            IOptions<ShoppingListDatabaseSettings> shoppingListDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                shoppingListDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                shoppingListDatabaseSettings.Value.DatabaseName);

            _ShoppingListCollection = mongoDatabase.GetCollection<ShoppingList>(
                shoppingListDatabaseSettings.Value.ShoppingListCollectionName);
        }

        public async Task<List<ShoppingList>> GetAsync() =>
            await _ShoppingListCollection.Find(_ => true).ToListAsync();

        public async Task<ShoppingList?> GetAsync(string id) =>
            await _ShoppingListCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ShoppingList newShoppingList) =>
            await _ShoppingListCollection.InsertOneAsync(newShoppingList);

        public async Task UpdateAsync(string id, ShoppingList updateShoppingList) =>
            await _ShoppingListCollection.ReplaceOneAsync(x => x.Id == id, updateShoppingList);

        public async Task RemoveAsync(string id) =>
            await _ShoppingListCollection.DeleteOneAsync(x => x.Id == id);
    }
}
