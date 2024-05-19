using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace _334_group_project_web_api.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsDelivered { get; set; }
        public string Status { get; set; }
        public string deliveryAddress { get; set; }
        public ShoppingList ShoppingList { get; set; }
    }
}
