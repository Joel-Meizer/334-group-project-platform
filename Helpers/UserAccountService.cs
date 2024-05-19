using _334_group_project_web_api.DBSettings;
using _334_group_project_web_api.Models;
using _334_group_project_web_api.Services;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using MongoDB.Driver;

namespace _334_group_project_web_api.Helpers;

public class UserAccountService
{
    private readonly IMongoCollection<UserAccount> _UserAccountCollection;
    private readonly FamilyService _familyService;
    private readonly InventoryService _inventoryService;

    public UserAccountService(IOptions<UserAccountDatabaseSettings> userAccountDatabaseSettings, FamilyService familyService, InventoryService inventoryService)
    {
        var mongoClient = new MongoClient(
            userAccountDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            userAccountDatabaseSettings.Value.DatabaseName);

        _UserAccountCollection = mongoDatabase.GetCollection<UserAccount>(
            userAccountDatabaseSettings.Value.UserAccountCollectionName);

        _familyService = familyService;

        _inventoryService = inventoryService;
    }

    public async Task<List<UserAccount>> GetAsync() =>
        await _UserAccountCollection.Find(_ => true).ToListAsync();

    public async Task<UserAccount?> GetAsync(string id) =>
        await _UserAccountCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(UserAccount userAccount)
    {

        //Give user Admin if new User for new Family
        if (userAccount.Type == null)
        {
            userAccount.Type = UserAccountType.Administrator;
        }


        // Set default permissions based on user type
        switch (userAccount.Type)
        {
            case UserAccountType.Administrator:
                userAccount.CanManageInventory = true;
                userAccount.CanManageOrders = true;
                userAccount.CanManageUsers = true;
                // Set other permissions as needed
                break;
            case UserAccountType.AdultUser:
                userAccount.CanManageInventory = true;
                userAccount.CanManageOrders = true;
                userAccount.CanManageUsers = true;
                // Set other permissions as needed
                break;
            case UserAccountType.ChildUser:
                userAccount.CanManageInventory = true;
                userAccount.CanManageOrders = false;
                userAccount.CanManageUsers = false;
                // Set other permissions as needed
                break;
        }



        await _UserAccountCollection.InsertOneAsync(userAccount);

        // If the user is an admin, create a new family and assign the FamilyId
        if (userAccount.Type == UserAccountType.Administrator)
        {
            var newFamily = new Family
            {
                AdminUserId = userAccount.Id,
            };



            await _familyService.CreateAsync(newFamily);            
            
            var newInventory = new Inventory
            {
                familyId = newFamily.Id,
            };

            var invId = await _inventoryService.CreateAsync(newInventory);
            userAccount.FamilyId = newFamily.Id;
            newFamily.InventoryId = invId;
            await _familyService.UpdateAsync(newFamily.Id, newFamily);
        }

        await _UserAccountCollection.ReplaceOneAsync(x => x.Id == userAccount.Id, userAccount);
    }


    public async Task UpdateAsync(string id, UserAccount updateUser) =>
        await _UserAccountCollection.ReplaceOneAsync(x => x.Id == id, updateUser);

    public async Task RemoveAsync(string id) =>
        await _UserAccountCollection.DeleteOneAsync(x => x.Id == id);

    public async Task<UserAccount>? GetAsyncByCreds(string email, string password) =>
        await _UserAccountCollection.Find(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
}


