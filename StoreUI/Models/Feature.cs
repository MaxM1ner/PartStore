using System.ComponentModel.DataAnnotations.Schema;

namespace StoreUI.Models
{
    public sealed class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
    }
}
