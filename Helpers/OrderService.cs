using _334_group_project_web_api.DBSettings;
using _334_group_project_web_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace _334_group_project_web_api.Helpers
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _OrderCollection;

        public OrderService(
            IOptions<OrderDatabaseSettings> orderDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                orderDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                orderDatabaseSettings.Value.DatabaseName);

            _OrderCollection = mongoDatabase.GetCollection<Order>(
                orderDatabaseSettings.Value.OrdersCollectionName);
        }

        public async Task<List<Order>> GetAsync() =>
            await _OrderCollection.Find(_ => true).ToListAsync();

        public async Task<Order?> GetAsync(string id) =>
            await _OrderCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Order newOrder) =>
            await _OrderCollection.InsertOneAsync(newOrder);

        public async Task UpdateAsync(string id, Order updateOrder) =>
            await _OrderCollection.ReplaceOneAsync(x => x.Id == id, updateOrder);

        public async Task RemoveAsync(string id) =>
            await _OrderCollection.DeleteOneAsync(x => x.Id == id);
    }
}
