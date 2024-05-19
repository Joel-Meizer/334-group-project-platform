using _334_group_project_web_api.DBSettings;
using _334_group_project_web_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _334_group_project_web_api.Services
{
    public class InventoryService
    {
        private readonly IMongoCollection<Inventory> _InventoryCollection;

        public InventoryService(IOptions<InventoryDatabaseSettings> inventoryDatabaseSettings)
        {
            var mongoClient = new MongoClient(inventoryDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(inventoryDatabaseSettings.Value.DatabaseName);
            _InventoryCollection = mongoDatabase.GetCollection<Inventory>(
                inventoryDatabaseSettings.Value.InventorysCollectionName);
        }


        public async Task<List<Inventory>> GetAllInventoryItems()
        {
            return await _InventoryCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Inventory> GetInventoryItem(string id)
        {
            return await _InventoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> CreateAsync(Inventory newInventory)
        {
            await _InventoryCollection.InsertOneAsync(newInventory);
            return newInventory.Id;
        }
            



        public async Task UpdateAsync(string id, Inventory inventory)
        {
            await _InventoryCollection.ReplaceOneAsync(x => x.Id == id, inventory);
        }

        public async Task DeleteAsync(string id)
        {
            await _InventoryCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
