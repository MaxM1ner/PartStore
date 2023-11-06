using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public sealed class CartProduct
    {
        public int CartProductId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
        public string CustomerId { get; set; } = null!;
        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }
    }
}
