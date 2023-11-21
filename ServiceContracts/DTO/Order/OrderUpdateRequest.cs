using Entities.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Order
{
    public class OrderUpdateRequest
    {
        public OrderUpdateRequest(int orderId, decimal price, OrderStatus status, string adress, Guid customerId)
        {
            OrderId = orderId;
            TotalPrice = price;
            Status = status;
            Address = adress;
            CustomerId = customerId;
        }
        public int OrderId { get; private set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public string Address { get; set; } = null!;
        public Guid CustomerId { get; private set; }

        public CustomerOrder ToCustomerOrder()
        {
            return new CustomerOrder()
            {
                CustomerId = this.CustomerId.ToString(),
                Status = Status.ToString(),
                TotalPrice = this.TotalPrice,
                Address = this.Address
            };
        }
    }
}
