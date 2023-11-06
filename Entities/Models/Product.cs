using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Entities.Models
{
    public sealed class Product
    {
        public Product()
        {
            this.Features = new HashSet<Feature>();
            this.Comments = new HashSet<ProductComment>();
            this.Images = new HashSet<ProductImage>();
        }
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public bool IsVisible {  get; set; }
        public int ProductTypeId { get; set; }
        [ForeignKey(nameof(ProductTypeId))]
        public ProductType? Type { get; set; }
        public ICollection<Feature> Features { get; private set; }
        public ICollection<ProductComment> Comments { get; private set; }
        public ICollection<ProductImage> Images { get; private set; }
    }
}
