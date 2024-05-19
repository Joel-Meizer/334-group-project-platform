using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace _334_group_project_web_api.Models
{
    public class Family
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string AdminUserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? InventoryId { get; set; }

        public List<UserAccount> FamilyMembers { get; set; } = new List<UserAccount>();

    }
}
