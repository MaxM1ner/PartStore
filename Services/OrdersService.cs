using DataAccess;
using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.Cart;
using ServiceContracts.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;

        public OrdersService(ApplicationDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<OrderResponse?> AddOrderAsync(OrderAddRequest? CustomerOrder)
        {
            if (CustomerOrder == null) throw new ArgumentNullException(nameof(CustomerOrder));
            if (CustomerOrder.CustomerId == Guid.Empty) throw new ArgumentException(nameof(CustomerOrder.CustomerId));

            CustomerOrder.OrderProducts = await _cartService.GetAllProductsAsync(CustomerOrder.CustomerId.ToString());
            CustomerOrder.UpdateOrderPrice();

            var newOrder = await _context.CustomerOrders.AddAsync(new CustomerOrder()
            {
                CustomerId = CustomerOrder.CustomerId.ToString(),
                CreatedTimestamp = DateTime.Now,
                Address = CustomerOrder.Address,
                Status = OrderStatus.Created.ToString(),
                TotalPrice = CustomerOrder.TotalPrice
            });
            foreach (var orderedProduct in CustomerOrder.OrderProducts)
            {
                Product? dbProduct = await _context.Products.FindAsync(orderedProduct.ProductId);
                if (dbProduct == null) continue;
                if (dbProduct.Quantity < 1) continue;
                dbProduct.Quantity--;
                newOrder.Entity.OrderProducts.Add(dbProduct);
            }
            await _context.SaveChangesAsync();
            await newOrder.Collection(x => x.OrderProducts).LoadAsync();
            await newOrder.Reference(x => x.Customer).LoadAsync();
            await _cartService.ClearCartAsync(newOrder.Entity.CustomerId);
            return newOrder.Entity.ToOrderResponse();
        }

        public async Task<List<OrderResponse>> GetAllOrdersAsync(string? CustomerId)
        {
            if (CustomerId == null) throw new ArgumentNullException(nameof(CustomerId));

            if ((await _context.Customers.FindAsync(CustomerId.ToString())) == null) throw new ArgumentException(nameof(CustomerId));

            return (await _context.CustomerOrders.Include(x => x.OrderProducts).Where(x => x.CustomerId == CustomerId.ToString()).ToListAsync()).Select(x => x.ToOrderResponse()).ToList();
        }

        public async Task<OrderResponse>? GetOrderAsync(int OrderId)
        {
            if (OrderId < 0) throw new ArgumentException($"CartProductId {OrderId} can't be lower than 0");

            var order = await _context.CustomerOrders.Include(x => x.OrderProducts).Where(x => x.Id == OrderId).FirstAsync();
            if (order == null) throw new ArgumentException(nameof(OrderId));

            return order.ToOrderResponse();
        }

        public async Task<bool> RemoveOrderAsync(OrderResponse? CustomerOrder)
        {
            if (CustomerOrder == null) throw new ArgumentNullException(nameof(CustomerOrder));

            if (CustomerOrder.CustomerId == Guid.Empty) throw new ArgumentException(nameof(CustomerOrder.CustomerId));

            var dbOrder = await _context.CustomerOrders.Where(x => x.Id == CustomerOrder.OrderId).FirstOrDefaultAsync();
            if (dbOrder == null) return false;

            _context.CustomerOrders.Remove(dbOrder);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OrderResponse> UpdateOrderAsync(OrderUpdateRequest? UpdateOrder)
        {
            if (UpdateOrder == null) throw new ArgumentNullException(nameof(UpdateOrder));

            if (UpdateOrder.CustomerId == Guid.Empty) throw new ArgumentException(nameof(UpdateOrder.CustomerId));

            OrderResponse order = await GetOrderAsync(UpdateOrder.OrderId);
            if (order == null) throw new ArgumentException(nameof(UpdateOrder));

            var updatedOrder = _context.CustomerOrders.Update(UpdateOrder.ToCustomerOrder());
            await _context.SaveChangesAsync();
            await updatedOrder.Collection(x => x.OrderProducts).LoadAsync();
            await updatedOrder.Reference(x => x.Customer).LoadAsync();
            return updatedOrder.Entity.ToOrderResponse();
        }
    }
}
