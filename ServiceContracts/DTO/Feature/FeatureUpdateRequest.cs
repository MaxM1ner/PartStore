using Entities.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Order
{
    public class FeatureUpdateRequest
    {
        public FeatureUpdateRequest(int featureId, string name, string value, int productTypeId, IEnumerable<int> productIds)
        {
            if (featureId < 0) throw new ArgumentException(nameof(featureId));
            FeatureId = featureId;
            if (name == string.Empty || name is null) throw new ArgumentNullException(nameof(name));
            Name = name;
            if (value == string.Empty || value is null) throw new ArgumentNullException(nameof(value));
            Value = value;
            if (productTypeId < 0) throw new ArgumentException(nameof(productTypeId));
            ProductTypeId = productTypeId;
            if (productIds is null) throw new ArgumentNullException(nameof(productIds));
            ProductIds = productIds;    
        }
        public int FeatureId { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int ProductTypeId { get; set; }
        public IEnumerable<int> ProductIds { get; private set; }

    }
}
