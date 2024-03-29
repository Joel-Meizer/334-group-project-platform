using System;
using System.Collections.Generic;

namespace _334_group_project_web_api.Models
{
    public enum ShoppingListStatus
    {
        Active,
        Inactive,
        Deleted,
        Ordered
    }

    public class ShoppingList
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public List<string> ProductIds { get; set; } = new List<string>(); // List of Product Ids
        public string DisplayName { get; set; }
        public ShoppingListStatus Status { get; set; } = ShoppingListStatus.Active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }
        public DateTime? OrderedAt { get; set; }
    }
}
