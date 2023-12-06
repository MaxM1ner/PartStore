using Microsoft.CodeAnalysis.CSharp.Syntax;
using ServiceContracts.DTO.Feature;

namespace StoreUI.Areas.Admin.ViewModels
{
    public sealed class FeatureViewModel
    {
        public FeatureViewModel()
        {
            ProductIds = new List<int>();
        }
        public int FeatureId { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int ProductTypeId { get; set; }
        public string? ProductTypeValue { get; set; }
        public IEnumerable<int> ProductIds { get; set; }

        public FeatureAddRequest ToFeatureAddRequest()
        {
            return new FeatureAddRequest(Name, Value, ProductTypeId, ProductIds);
        }
        public FeatureUpdateRequest ToFeatureUpdateRequest()
        {
            return new FeatureUpdateRequest(FeatureId, Name, Value, ProductTypeId, ProductIds);
        }
    }

    public static class FeatureResponseExtensions
    {
        public static FeatureViewModel ToFeatureViewModel(this FeatureResponse response)
        {
            return new FeatureViewModel()
            { 
                FeatureId = response.FeatureId,
                Name = response.Name,
                Value = response.Value,
                ProductTypeId = response.ProductTypeId,
                ProductIds = response.ProductIds,
                ProductTypeValue = response.ProductTypeValue
            };
        }
    }
}
