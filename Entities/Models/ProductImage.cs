using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public sealed class ProductImage
    {
        public int Id { get; set; }
        public string Path { get; set; } = null!;
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
    }
}
