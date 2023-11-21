using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
        public OrderAddRequest(Guid customerId, ICollection<Product> products, string address) 
        {
            OrderProducts = products;
            CustomerId = customerId;
            Address = address;
            foreach (Product product in products) 
            {
                TotalPrice += product.Price;
            }
            CreatedDate = DateTime.Now;
        }
        public Guid CustomerId { get; private set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; private set; }
        public decimal TotalPrice { get; private set; }
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        public ICollection<Product> OrderProducts { get; private set; }

        public CustomerOrder ToCustomerOrder()
        {
            return new CustomerOrder()
            {
                CustomerId = this.CustomerId.ToString(),
                CreatedTimestamp = CreatedDate,
                OrderProducts = this.OrderProducts,
                TotalPrice = this.TotalPrice,
                Address = this.Address                
            };
        }
    }
}
