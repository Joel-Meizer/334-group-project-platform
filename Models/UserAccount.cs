using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace _334_group_project_web_api.Models
{
    public enum UserAccountType
    {
        Administrator,
        AdultUser,
        ChildUser
    }
    public class UserAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? FamilyId { get; set; }

        public string? givenName { get; set; }
        public string? surname { get; set; }
        public string? displayName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? EmailConfirmed { get; set; }
        public UserAccountType? Type {  get; set; }
        public string? PhoneNumber { get; set; }
        public string? street { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? postalCode { get; set; }
        public string? country { get; set; }
        public List<string> prderIds { get; set; } = new List<string>();


        //Properties for permissions
        public bool CanManageInventory { get; set; }
        public bool CanManageOrders { get; set; }
        public bool CanManageUsers { get; set; }


    }
}
