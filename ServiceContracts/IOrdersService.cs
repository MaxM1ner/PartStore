using Entities.Models;
using ServiceContracts.DTO.Cart;
using ServiceContracts.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    /// <summary>
    /// Represents orders service methods to use customer's orders
    /// </summary>
    public interface IOrdersService
    {
        /// <summary>
        /// Add a new order to the database
        /// </summary>
        /// <param name="CustomerOrder">Order add request</param>
        /// <returns>Added order response</returns>
        Task<OrderResponse?> AddOrderAsync(OrderAddRequest? CustomerOrder);

        /// <summary>
        /// Find order by its id
        /// </summary>
        /// <param name="OrderId">Id of the order</param>
        /// <returns>Response of the found order, otherwise null</returns>
        Task<OrderResponse>? GetOrderAsync(int OrderId);

        /// <summary>
        /// Get all orders of the specified customer
        /// </summary>
        /// <param name="CustomerId">Id of the customer</param>
        /// <returns>List of customer's orders</returns>
        Task<List<OrderResponse>> GetAllOrdersAsync(string? CustomerId);

        /// <summary>
        /// Update order properties
        /// </summary>
        /// <param name="UpdateOrder">Order update request</param>
        /// <returns>Updated order response</returns>
        Task<OrderResponse> UpdateOrderAsync(OrderUpdateRequest? UpdateOrder);

        /// <summary>
        /// Remove customer's order from the database
        /// </summary>
        /// <param name="CustomerOrder">Order to remove</param>
        /// <returns>True if sucess, otherwise false</returns>
        Task<bool> RemoveOrderAsync(OrderResponse? CustomerOrder);
    }
}
