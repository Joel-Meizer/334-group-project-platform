using _334_group_project_web_api.DBSettings;
using _334_group_project_web_api.Models;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using MongoDB.Driver;

namespace _334_group_project_web_api.Helpers;

public class UserAccountService
{
    private readonly IMongoCollection<UserAccount> _UserAccountCollection;

    public UserAccountService(
        IOptions<UserAccountDatabaseSettings> userAccountDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            userAccountDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            userAccountDatabaseSettings.Value.DatabaseName);

        _UserAccountCollection = mongoDatabase.GetCollection<UserAccount>(
            userAccountDatabaseSettings.Value.UserAccountCollectionName);
    }

    public async Task<List<UserAccount>> GetAsync() =>
        await _UserAccountCollection.Find(_ => true).ToListAsync();

    public async Task<UserAccount?> GetAsync(string id) =>
        await _UserAccountCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(UserAccount newUser) =>
        await _UserAccountCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, UserAccount updateUser) =>
        await _UserAccountCollection.ReplaceOneAsync(x => x.Id == id, updateUser);

    public async Task RemoveAsync(string id) =>
        await _UserAccountCollection.DeleteOneAsync(x => x.Id == id);

    public async Task<UserAccount>? GetAsyncByCreds(string email, string password) =>
        await _UserAccountCollection.Find(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
}


