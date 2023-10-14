using System.Runtime.CompilerServices;

namespace StoreUI.Models
{
    public sealed class Customer
    {
        public Customer() 
        {
            this.CartProducts = new HashSet<CartProduct>();
            this.Comments = new HashSet<ProductComment>();
        }
        public string Id { get; set; } = null!;
        public ICollection<CartProduct> CartProducts { get; private set; }
        public ICollection<ProductComment> Comments { get; private set; }
    }
}
