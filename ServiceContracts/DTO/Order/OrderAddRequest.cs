using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ServiceContracts.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Order
{
    /// <summary>
    /// DTO class to add a new customer order
    /// </summary>
    public class OrderAddRequest
    {
        public OrderAddRequest(Guid customerId, string address) 
        {
            OrderProducts = new HashSet<CartProductResponse>();
            CustomerId = customerId;
            Address = address;
            CreatedDate = DateTime.Now;
        }
        public Guid CustomerId { get; private set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; private set; }
        public decimal TotalPrice { get; private set; }
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        public ICollection<CartProductResponse> OrderProducts { get; set; }

        public CustomerOrder ToCustomerOrder()
        {
            return new CustomerOrder()
            {
                CustomerId = this.CustomerId.ToString(),
                CreatedTimestamp = CreatedDate,
                OrderProducts = this.OrderProducts.Select(x => x.ToProduct()).ToHashSet(),
                TotalPrice = this.TotalPrice,
                Address = this.Address                
            };
        }
        public decimal UpdateOrderPrice()
        {
            foreach(var cartProduct in OrderProducts)
            {
                this.TotalPrice += cartProduct.ProductPrice * cartProduct.Quantity;
            }
            return this.TotalPrice;
        }
    }
}
