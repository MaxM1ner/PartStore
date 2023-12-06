using Entities.Models;
using ServiceContracts.DTO.Feature;
using ServiceContracts.DTO.Order;
using ServiceContracts.DTO.Product;
using ServiceContracts.DTO.ProductType;
using Services;

namespace StoreUI.Areas.Admin.ViewModels
{
    public sealed class ProductTypeViewModel
    {
        public ProductTypeViewModel()
        {
            this.Products = new HashSet<ProductResponse>();
            this.Features = new HashSet<FeatureResponse>();
        }
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public bool IsVisible { get; set; }
        public string? TypeImagePath { get; set; }
        public IFormFile? TypeImage { get; set; }
        public ICollection<FeatureResponse> Features { get; private set; }
        public ICollection<ProductResponse> Products { get; private set; }

        public ProductTypeAddRequest ToProductTypeAddRequest()
        {
            return new ProductTypeAddRequest()
            {
                Value = Value,
                Visible = IsVisible,
                TypeImagepath = TypeImagePath
            };
        }

        public ProductTypeUpdateRequest ToProductTypeUpdateRequest()
        {
            return new ProductTypeUpdateRequest(Id)
            {
                TypeImagepath = TypeImagePath,
                Value = Value,
                Visible = IsVisible,
            };
        }
    }
    public static class ProductTypeResponseExtensions
    {
        public static ProductTypeViewModel ToProductTypeViewModel(this ProductTypeResponse response)
        {
            var type = new ProductTypeViewModel()
            {
                Id = response.Id,
                IsVisible = response.Visible,
                TypeImagePath = response.TypeImagepath,
                Value = response.Value
            };
            foreach (var feature in response.Features)
            {
                type.Features.Add(feature);
            }
            foreach (var product in response.Products)
            {
                type.Products.Add(product);
            }
            return type;
        }
    }
}
