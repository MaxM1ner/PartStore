namespace StoreUI.Models
{
    public sealed class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ProductType Type { get; set; } = null!;
        public ICollection<Feature> Features { get; set; } = new HashSet<Feature>();
        public ICollection<ProductComment> Comments { get; set; } = new HashSet<ProductComment>();
        public ICollection<ProductImage> Images { get; set; } = new HashSet<ProductImage>();
    }
}
