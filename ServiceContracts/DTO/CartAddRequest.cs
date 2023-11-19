using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class to add a new cart product
    /// </summary>
    public class CartAddRequest
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public CartAddRequest(Guid customerId, int productId, int quantity = 1) 
        {
            Quantity = quantity;
            CustomerId = customerId;
            ProductId = productId;
        }

        public CartProduct ToCartProduct() 
        {
            return new CartProduct()
            {
                Quantity = Quantity,
                CustomerId = this.CustomerId.ToString(),
                ProductId = this.ProductId              
            };
        }
    }
}
