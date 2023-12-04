using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ServiceContracts.DTO.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Feature
{
    /// <summary>
    /// DTO class to add a new customer order
    /// </summary>
    public sealed class FeatureAddRequest
    {
        public FeatureAddRequest(string featureName, string value, int productTypeId, IEnumerable<int> productIds) 
        {
            if (featureName == string.Empty || featureName is null) throw new ArgumentNullException(nameof(featureName));
            Name = featureName;
            if (value == string.Empty || value is null) throw new ArgumentNullException(nameof(value));
            Value = value;
            if (productTypeId < 0) throw new ArgumentException(nameof(productTypeId));
            ProductTypeId = productTypeId;
            if (productIds is null) throw new ArgumentNullException(nameof(productIds));
            ProductIds = productIds;


        }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int ProductTypeId { get; set; }
        public IEnumerable<int> ProductIds { get; private set; }
        public Entities.Models.Feature ToFeature()
        {
            var newFeature = new Entities.Models.Feature()
            {
                Name = this.Name,
                ProductTypeId = this.ProductTypeId,
                Value = this.Value,
            };
            return newFeature;
        }
    }
}
