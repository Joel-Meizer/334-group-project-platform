using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace _334_group_project_web_api.Models
{
    public class MealPlan
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int price { get; set; }
        public List<Meal> Meals { get; set; } = new List<Meal>();
        public string GrocerId { get; set; }
        public string imageUrl { get; set; }
    }
}
