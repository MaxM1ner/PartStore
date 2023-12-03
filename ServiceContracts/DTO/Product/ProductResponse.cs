using Entities.Enums;
using Entities.Models;
using ServiceContracts.DTO.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Order
{
    /// <summary>
    /// DTO class used as return type of ProductManager methods
    /// </summary>
    public class ProductResponse
    {
        public ProductResponse()
        {
            this.Features = new HashSet<FeatureResponse>();
            this.Comments = new HashSet<CommentResponse>();
            this.Images = new HashSet<ImageResponse>();
            this.Orders = new HashSet<OrderResponse>();
        }
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public bool IsVisible { get; set; }
        public int ProductTypeId { get; set; }
        public ICollection<FeatureResponse> Features { get; set; }
        public ICollection<CommentResponse> Comments { get; set; }
        public ICollection<ImageResponse> Images { get; set; }
        public ICollection<OrderResponse> Orders { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(ProductResponse)) return false;

            ProductResponse? toCompare = obj as ProductResponse;
            if (toCompare == null) return false;

            return toCompare.Id == Id &&
                toCompare.ProductTypeId == ProductTypeId;
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

    public static class ProductExtensions
    {
        public static ProductResponse ToProductResponse(this Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                ProductTypeId = product.ProductTypeId,
                IsVisible = product.IsVisible,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Comments = product.Comments.Select(x => x.ToCommentResponse()).ToHashSet(),
                Features = product.Features.Select(x => x.ToFeatureResponse()).ToHashSet(),
                Images = product.Images.Select(x => x.ToImageResponse()).ToHashSet(),
                Orders = product.Orders.Select(x => x.ToOrderResponse()).ToHashSet()
            };
        }
    }
}
