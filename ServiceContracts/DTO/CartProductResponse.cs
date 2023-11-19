using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class used as return type of CartService methods
    /// </summary>
    public class CartProductResponse
    {
        public int CartProductId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Guid CustomerId { get; set; }

        public decimal ProductPrice { get; set; }
        public string? ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public string? ProductImagePath { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(CartProductResponse)) return false;

            CartProductResponse? toCompare = obj as CartProductResponse;
            if (toCompare == null) return false;

            return toCompare.CartProductId == CartProductId &&
                toCompare.Quantity == Quantity &&
                toCompare.ProductId == ProductId &&
                toCompare.CustomerId == CustomerId;
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

    public static class CartProductExtensions
    {
        public static CartProductResponse ToCartProductResponse(this CartProduct product)
        {
            return new CartProductResponse()
            {
                CartProductId = product.CartProductId,
                Quantity = product.Quantity,
                ProductId = product.ProductId,
                CustomerId = Guid.Parse(product.CustomerId),
                ProductName = product.Product?.Name,
                ProductPrice = product.Product.Price,
                ProductQuantity = product.Product.Quantity,
                ProductImagePath = product.Product.Images.FirstOrDefault()?.Path
            };
        }
    }
}
