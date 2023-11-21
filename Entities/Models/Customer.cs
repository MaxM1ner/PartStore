using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Entities.Models
{
    public sealed class Customer : IdentityUser
    {
        public Customer() 
        {
            this.CartProducts = new HashSet<CartProduct>();
            this.Comments = new HashSet<ProductComment>();
            this.Orders = new HashSet<CustomerOrder>();
        }
        public ICollection<CartProduct> CartProducts { get; private set; }
        public ICollection<CustomerOrder> Orders { get; private set; }
        public ICollection<ProductComment> Comments { get; private set; }
    }
}
