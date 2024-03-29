namespace _334_group_project_web_api.Models
{
    public enum ProductType
    {
        Confectionary,
        Deli,
        ProteinMeat,
        Cereal,
        FrozenFoods,
        HealthFoods,
        BakeryItems,
        DairyProducts,
        Produce,
        Beverages,
        Snacks,
        Condiments,
        CannedGoods,
        Pasta,
        Sauces,
        Spices
    }

    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ProductType? Category { get; set; }
        public double? Price { get; set; }
        public string? Manufacturer { get; set; }
        public double? WeightOrQuantity { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? NutritionalInformation { get; set; }
        public string? Ingredients { get; set; }
        public string? CountryOfOrigin { get; set; }
        public string? Barcode { get; set; }
        public string? ImageUrl { get; set; }
        public string? UnitOfMeasurement { get; set; }
        public double? DiscountPrice { get; set; }
    }
}
