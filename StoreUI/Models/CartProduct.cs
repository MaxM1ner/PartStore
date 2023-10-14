using System.ComponentModel.DataAnnotations.Schema;

namespace StoreUI.Models
{
    public sealed class CartProduct
    {
        public int CartProductId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;
    }
}
