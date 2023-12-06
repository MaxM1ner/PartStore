using Entities.Enums;
using Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreUI.Areas.Admin.ViewModels
{
    public sealed class OrderViewModel
    {
        public OrderViewModel()
        {
            OrderProducts = new HashSet<Product>();
        }
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public string Status { get; set; } = OrderStatus.Created.ToString();
        public string Address { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public ICollection<Product> OrderProducts { get; private set; }
    }
    
}
