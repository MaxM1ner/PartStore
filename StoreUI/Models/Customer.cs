using System.Runtime.CompilerServices;

namespace StoreUI.Models
{
    public sealed class Customer
    {
        public string Id { get; set; } = null!;
        public ICollection<CartProduct> CartProducts { get; set; } = new HashSet<CartProduct>();
        public ICollection<ProductComment> Comments { get; set; } = new HashSet<ProductComment>();
    }
}
