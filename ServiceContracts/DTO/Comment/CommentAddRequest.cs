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
    public class CommentAddRequest
    {
        public CommentAddRequest(string customerId, int productId, string value) 
        {
            if (customerId == string.Empty || customerId is null) throw new ArgumentNullException(nameof(customerId));
            CustomerId = customerId;
            if (value == string.Empty || value is null) throw new ArgumentNullException(nameof(value));
            Value = value;
            ProductId = productId;
        }
        public string CustomerId { get; private set; }
        public int ProductId { get; private set; }
        public string Value { get; private set; } = string.Empty;

        public ProductComment ToProductComment()
        {
            return new ProductComment()
            {
                CustomerId = this.CustomerId.ToString(),
                ProductId = this.ProductId,
                Value = this.Value
            };
        }
    }
}
