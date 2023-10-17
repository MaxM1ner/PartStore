using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreUI.Models
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
