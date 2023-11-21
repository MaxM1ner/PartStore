using Entities.Models;
using Services;

namespace StoreUI.Areas.Admin.ViewModels
{
    public class ProductTypeViewModel
    {
        public ProductTypeViewModel()
        {
            this.Products = new HashSet<Product>();
            this.Features = new HashSet<Feature>();
        }
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public IFormFile TypeImage { get; set; } = null!;
        public ICollection<Feature> Features { get; private set; }
        public ICollection<Product> Products { get; private set; }

        public ProductType ToProduct()
        {
            return new ProductType()
            {
                Id = Id,
                Value = Value
            };
        }
    }
}
