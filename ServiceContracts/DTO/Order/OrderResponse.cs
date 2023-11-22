using Entities.Enums;
using Entities.Models;
using ServiceContracts.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Order
{
    /// <summary>
    /// DTO class used as return type of OrdersService methods
    /// </summary>
    public class OrderResponse
    {
        public OrderResponse() 
        {
            OrderedProductIds = new HashSet<int>();
        }
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public string Address { get; set; } = null!;
        public Guid CustomerId { get; set; }
        public IEnumerable<int> OrderedProductIds { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(OrderResponse)) return false;

            OrderResponse? toCompare = obj as OrderResponse;
            if (toCompare == null) return false;

            return toCompare.OrderId == OrderId &&
                toCompare.CustomerId == CustomerId &&
                toCompare.TotalPrice == TotalPrice;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string? ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public static class CustomerOrderExtensions
    {
        public static OrderResponse ToOrderResponse(this CustomerOrder order)
        {
            return new OrderResponse
            {
                CreatedTimestamp = order.CreatedTimestamp,
                CustomerId = Guid.Parse(order.CustomerId),
                OrderId = order.Id,
                Status = Enum.Parse<OrderStatus>(order.Status),
                TotalPrice = order.TotalPrice,
                OrderedProductIds = order.OrderProducts.Select(x => x.ProductId),
                Address = order.Address
            };
        }
    }
}
