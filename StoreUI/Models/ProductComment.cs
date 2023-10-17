using System.ComponentModel.DataAnnotations.Schema;

namespace StoreUI.Models
{
    public sealed class ProductComment
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public int ProductId { get; set; }
        public string CustomerId { get; set; } = null!;
        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
    }
}
