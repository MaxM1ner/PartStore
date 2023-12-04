using Entities.Models;
using ServiceContracts.DTO.Feature;
using ServiceContracts.DTO.Order;
using ServiceContracts.DTO.Product;
using Services;

namespace StoreUI.Areas.Admin.ViewModels
{
    public class ProductTypeViewModel
    {
        public ProductTypeViewModel()
        {
            this.Products = new HashSet<ProductResponse>();
            this.Features = new HashSet<FeatureResponse>();
        }
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public bool IsVisible { get; set; }
        public IFormFile TypeImage { get; set; } = null!;
        public ICollection<FeatureResponse> Features { get; private set; }
        public ICollection<ProductResponse> Products { get; private set; }

        public ProductType ToProduct()
        {
            return new ProductType()
            {
                Id = Id,
                Value = Value,
                Visible = IsVisible
            };
        }
    }
}
