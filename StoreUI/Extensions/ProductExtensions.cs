using Entities.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
                Features = product.Features,
                Comments = product.Comments,
                Images = product.Images
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
