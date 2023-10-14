namespace StoreUI.Models
{
    public sealed class ProductImage
    {
        public int Id { get; set; }
        public string Path { get; set; } = null!;
        public int ProductId { get; set; }
        public Product product { get; set; } = null!;
    }
}
