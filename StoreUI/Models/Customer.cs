using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient.DataClassification;
using StoreUI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace StoreUI.Models
{
    public class Customer : IdentityUser
    {
        public Customer() 
        {
            this.CartProducts = new HashSet<CartProduct>();
            this.Comments = new HashSet<ProductComment>();
        }
        public virtual ICollection<CartProduct> CartProducts { get; private set; }
        public virtual ICollection<ProductComment> Comments { get; private set; }
    }
}
