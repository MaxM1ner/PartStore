using Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreUI.Areas.Admin.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            this.Features = new HashSet<Feature>();
            this.Comments = new HashSet<ProductComment>();
            this.Images = new HashSet<ProductImage>();
            this.SelectedFeaturesIds = new HashSet<int>();
            this.FormImages = new HashSet<IFormFile>();
        }

        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public bool IsVisible { get; set; }
        public int ProductTypeId { get; set; }
        public string? TypeValue { get; set; }
        public ICollection<Feature> Features { get; set; }
        public ICollection<ProductComment> Comments { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public IEnumerable<int> SelectedFeaturesIds { get; set; }
        public ICollection<IFormFile> FormImages { get; set; }
        public Product ToProduct()
        {
            return new Product() 
            {
                Id = Id,
                Name = Name,
                Description = Description,
                IsVisible = IsVisible,
                ProductTypeId = ProductTypeId,
                Price = Price,
                Quantity = Quantity
            };
        }
    }
}
