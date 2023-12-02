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
    /// DTO class used as return type of ProductTypeManager methods
    /// </summary>
    public class ProductTypeResponse
    {
        public ProductTypeResponse()
        {
            this.Products = new HashSet<ProductResponse>();
            this.Features = new HashSet<Feature>();
        }
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public string TypeImagepath { get; set; } = null!;
        public bool Visible { get; set; } = true;
        public ICollection<Feature> Features { get; set; }
        public ICollection<ProductResponse> Products { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(ProductTypeResponse)) return false;

            ProductTypeResponse? toCompare = obj as ProductTypeResponse;
            if (toCompare == null) return false;

            return toCompare.Id == Id &&
                toCompare.Value == Value;
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

    public static class ProductTypeExtensions
    {
        public static ProductTypeResponse ToProductTypeResponse(this ProductType productType)
        {
            return new ProductTypeResponse
            {
                Id = productType.Id,
                Value = productType.Value,
                TypeImagepath = productType.TypeImagepath,
                Visible = productType.Visible,
                Features = productType.Features,
                Products = productType.Products.Select(x => x.ToProductResponse()).ToHashSet(),
            };
        }
    }
}
