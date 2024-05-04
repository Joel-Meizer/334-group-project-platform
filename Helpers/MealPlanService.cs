using _334_group_project_web_api.DBSettings;
using _334_group_project_web_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace _334_group_project_web_api.Helpers
{
    public class MealPlanService
    {
        private readonly IMongoCollection<MealPlan> _MealPlanCollection;

        public MealPlanService(
            IOptions<MealPlanDatabaseSettings> mealPlanDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                mealPlanDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mealPlanDatabaseSettings.Value.DatabaseName);

            _MealPlanCollection = mongoDatabase.GetCollection<MealPlan>(
                mealPlanDatabaseSettings.Value.MealPlansCollectionName);
        }

        public async Task<List<MealPlan>> GetAsync() =>
            await _MealPlanCollection.Find(_ => true).ToListAsync();

        public async Task<MealPlan?> GetAsync(string id) =>
            await _MealPlanCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(MealPlan newMealPlan) =>
            await _MealPlanCollection.InsertOneAsync(newMealPlan);

        public async Task UpdateAsync(string id, MealPlan updateMealPlan) =>
            await _MealPlanCollection.ReplaceOneAsync(x => x.Id == id, updateMealPlan);

        public async Task RemoveAsync(string id) =>
            await _MealPlanCollection.DeleteOneAsync(x => x.Id == id);
    }
}
