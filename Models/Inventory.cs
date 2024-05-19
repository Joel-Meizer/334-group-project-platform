using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace _334_group_project_web_api.Models
{
    public class Inventory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public int ReorderThreshold { get; set; }
        public int ReorderQuantity { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UpdatedByUserId { get; set; }
        public UserAccount UpdatedByUser { get; set; }
    }
}
