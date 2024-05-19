using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace _334_group_project_web_api.Models
{
    public enum UserAccountType
    {
        Administrator = 0,
        ChildUser = 1,
        AdolescentUser = 2,
        AdultUser = 3
    }

    public class Alert
    {
        public string AlertType { get; set; } // Warning, Error, Success, Internal
        public string AlertText { get; set; }
    }
    public class UserAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? givenName { get; set; }
        public string? surname { get; set; }
        public string? displayName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? EmailConfirmed { get; set; }
        public UserAccountType? type {  get; set; }
        public string? PhoneNumber { get; set; }
        public string? street { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? postalCode { get; set; }
        public string? country { get; set; }
        public List<string> orderIds { get; set; } = new List<string>();
        public List<Alert> alerts { get; set; } = new List<Alert>();
        public string? relatedShoppingListId { get; set; }

    }
}
