using _334_group_project_web_api.DBSettings;
using _334_group_project_web_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace _334_group_project_web_api.Helpers
{
    public class MealService
    {
        private readonly IMongoCollection<Meal> _MealCollection;

        public MealService(
            IOptions<MealDatabaseSettings> mealDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                mealDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mealDatabaseSettings.Value.DatabaseName);

            _MealCollection = mongoDatabase.GetCollection<Meal>(
                mealDatabaseSettings.Value.MealCollectionName);
        }

        public async Task<List<Meal>> GetAsync() =>
            await _MealCollection.Find(_ => true).ToListAsync();

        public async Task<Meal?> GetAsync(string id) =>
            await _MealCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Meal newMeal) =>
            await _MealCollection.InsertOneAsync(newMeal);

        public async Task UpdateAsync(string id, Meal updateMeal) =>
            await _MealCollection.ReplaceOneAsync(x => x.Id == id, updateMeal);

        public async Task RemoveAsync(string id) =>
            await _MealCollection.DeleteOneAsync(x => x.Id == id);
    }
}
