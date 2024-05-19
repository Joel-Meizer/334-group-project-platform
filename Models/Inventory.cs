using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace _334_group_project_web_api.Models
{
    public class Inventory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? familyId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

        public int? Quantity { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        public string? UpdatedByUserId { get; set; }
    }
}
