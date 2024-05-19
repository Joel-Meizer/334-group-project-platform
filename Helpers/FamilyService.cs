using _334_group_project_web_api.Models;
using _334_group_project_web_api.DBSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace _334_group_project_web_api.Helpers
{
    public class FamilyService
    {
        private readonly IMongoCollection<Family> _FamilyCollection;

        public FamilyService(IOptions<FamilyDatabaseSettings> familyDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                familyDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                familyDatabaseSettings.Value.DatabaseName);
            _FamilyCollection = mongoDatabase.GetCollection<Family>(
                familyDatabaseSettings.Value.FamiliesCollectionName);
        }

        public async Task CreateAsync(Family newFamily) =>
            await _FamilyCollection.InsertOneAsync(newFamily);

        public async Task<Family> GetFamilyById(string familyId)
        {
            return await _FamilyCollection.Find(x => x.Id == familyId).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string familyId, Family updatedFamily) =>
            await _FamilyCollection.ReplaceOneAsync(x => x.Id == familyId, updatedFamily);

    }
}
