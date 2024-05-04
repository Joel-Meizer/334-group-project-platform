using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace _334_group_project_web_api.Models
{
    public class Grocer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public List<string> ProductIds { get; set; } = new List<string>(); // List of Product Ids
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }
    }
}
