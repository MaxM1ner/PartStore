using System.ComponentModel.DataAnnotations.Schema;

namespace StoreUI.Models
{
    public class Feature
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
        public virtual ProductType? Type { get; set; }
        public virtual ICollection<Product> Products { get; private set; }
    }
}
