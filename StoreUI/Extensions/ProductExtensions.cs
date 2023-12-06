using Entities.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ServiceContracts.DTO.Comment;
using ServiceContracts.DTO.Feature;
using ServiceContracts.DTO.Image;
using StoreUI.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreUI.Extensions
{
    public static class ProductExtensions
    {
        public static ProductViewModel ToProductViewModel(this Product product)
        {
            return new ProductViewModel()
            {
                Id = product.Id,
                Description = product.Description,
                IsVisible = product.IsVisible,
                Name = product.Name,
                Price = product.Price,
                ProductTypeId = product.ProductTypeId,
                Quantity = product.Quantity,
                TypeValue = product.Type?.Value,
                Features = product.Features.Select(x => x.ToFeatureResponse()).ToHashSet(),
                Comments = product.Comments.Select(x => x.ToCommentResponse()).ToHashSet(),
                Images = product.Images.Select(x => x.ToImageResponse()).ToHashSet()
            };
        }

        public static void UpdateProduct(this Product product, ProductViewModel model)
        {
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.IsVisible = model.IsVisible;
            product.Name = model.Name;
            product.Description = model.Description;
            product.ProductTypeId = model.ProductTypeId;
        }
    }
}
