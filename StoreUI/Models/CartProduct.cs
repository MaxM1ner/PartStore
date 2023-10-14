namespace StoreUI.Models
{
    public sealed class CartProduct
    {
        public int CartProductId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
    }
}
