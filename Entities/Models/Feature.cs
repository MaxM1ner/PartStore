using System.ComponentModel.DataAnnotations.Schema;

namespace StoreUI.Models
{
    public sealed class Feature
    {
        public Feature()
        {
            this.Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int ProductTypeId { get; set; }
        [ForeignKey(nameof(ProductTypeId))]
        public ProductType? Type { get; set; }
        public ICollection<Product> Products { get; private set; }
    }
}
