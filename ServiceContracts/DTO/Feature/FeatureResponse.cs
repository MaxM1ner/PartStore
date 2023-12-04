using Entities.Enums;
using Entities.Models;
using ServiceContracts.DTO.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Feature
{
    public sealed class FeatureResponse
    {
        public FeatureResponse() 
        {
            ProductIds = new List<int>();
        }
        public int FeatureId { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int ProductTypeId { get; set; }
        public List<int> ProductIds { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(FeatureResponse)) return false;

            FeatureResponse? toCompare = obj as FeatureResponse;
            if (toCompare == null) return false;

            return toCompare.Value == Value &&
                toCompare.Name == Name &&
                toCompare.ProductTypeId == ProductTypeId &&
                toCompare.ProductIds.All(ProductIds.Contains) &&
                toCompare.FeatureId == FeatureId;

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

    public static class FeatureExtensions
    {
        public static FeatureResponse ToFeatureResponse(this Entities.Models.Feature feature)
        {
            var productIds = feature.Products.Select(x => x.Id);
            return new FeatureResponse
            {
                FeatureId = feature.Id,
                Name = feature.Name,
                Value = feature.Value,
                ProductTypeId = feature.ProductTypeId,
                ProductIds = (List<int>)productIds
            };
        }
    }
}
