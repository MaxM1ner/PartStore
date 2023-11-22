using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CustomerOrder
    {
        public CustomerOrder() 
        {
            OrderProducts = new HashSet<CartProduct>();
        }
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public string Status { get; set; } = OrderStatus.Created.ToString();
        public string Address { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }
        public ICollection<CartProduct> OrderProducts { get; set; }
    }
}
