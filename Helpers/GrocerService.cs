using _334_group_project_web_api.DBSettings;
using _334_group_project_web_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace _334_group_project_web_api.Helpers
{
    public class GrocerService
    {
        private readonly IMongoCollection<Grocer> _GrocerCollection;

        public GrocerService(
            IOptions<GrocerDatabaseSettings> grocerDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                grocerDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                grocerDatabaseSettings.Value.DatabaseName);

            _GrocerCollection = mongoDatabase.GetCollection<Grocer>(
                grocerDatabaseSettings.Value.GrocersCollectionName);
        }

        public async Task<List<Grocer>> GetAsync() =>
            await _GrocerCollection.Find(_ => true).ToListAsync();

        public async Task<Grocer?> GetAsync(string id) =>
            await _GrocerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Grocer newGrocer) =>
            await _GrocerCollection.InsertOneAsync(newGrocer);

        public async Task UpdateAsync(string id, Grocer updateGrocer) =>
            await _GrocerCollection.ReplaceOneAsync(x => x.Id == id, updateGrocer);

        public async Task RemoveAsync(string id) =>
            await _GrocerCollection.DeleteOneAsync(x => x.Id == id);
    }
}
