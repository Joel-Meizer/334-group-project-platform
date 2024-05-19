using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace _334_group_project_web_api.Models
{
    public class Meal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int KCAL { get; set; }
        public decimal Price { get; set; }
        public string Recipe { get; set; }
        public List<Product> Ingredients { get; set; } = new List<Product>();
        public string relatedGrocerId { get; set; }
        public string imageUrl { get; set; }
    }
}
