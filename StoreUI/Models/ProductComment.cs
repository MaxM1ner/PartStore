namespace StoreUI.Models
{
    public sealed class ProductComment
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public int ProductId { get; set; }
        public string CustomerId { get; set; } = null!;
        public Product product { get; set; } = null!;
    }
}
