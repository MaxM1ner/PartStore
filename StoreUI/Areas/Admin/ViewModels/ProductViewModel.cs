using Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts.DTO.Comment;
using ServiceContracts.DTO.Feature;
using ServiceContracts.DTO.Image;
using ServiceContracts.DTO.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreUI.Areas.Admin.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            this.Features = new HashSet<FeatureResponse>();
            this.Comments = new HashSet<CommentResponse>();
            this.Images = new HashSet<ImageResponse>();
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
        public ICollection<FeatureResponse> Features { get; set; }
        public ICollection<CommentResponse> Comments { get; set; }
        public ICollection<ImageResponse> Images { get; set; }
        public IEnumerable<int> SelectedFeaturesIds { get; set; }
        public ICollection<IFormFile> FormImages { get; set; }

        public ProductAddRequest ToProductAddRequest()
        {
            return new ProductAddRequest() 
            {
                Name = Name,
                Description = Description,
                IsVisible = IsVisible,
                ProductTypeId = ProductTypeId,
                Price = Price,
                Quantity = Quantity,
                Features = Features.Select(x => x.ToAddRequest()).ToHashSet(),
                Images = Images.Select(x => x.ToAddRequest()).ToHashSet()
            };
        }
        public ProductUpdateRequest ToProductUpdateRequest()
        {
            return new ProductUpdateRequest(Id)
            {
                Name = Name,
                Description = Description,
                IsVisible = IsVisible,
                ProductTypeId = ProductTypeId,
                Price = Price,
                Quantity = Quantity,
                Features = Features,
                Images = Images
            };
        }
    }

    public static class ProductResponseExtensions
    {
        public static ProductViewModel ToProductViewModel(this ProductResponse response)
        {
            return new ProductViewModel()
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description,
                IsVisible = response.IsVisible,
                ProductTypeId = response.TypeResponse.Id,
                TypeValue = response.TypeResponse.Value,
                Price = response.Price,
                Quantity = response.Quantity,
                Comments = response.Comments,
                Features = response.Features,
                Images = response.Images
            };
        }
    }
}
