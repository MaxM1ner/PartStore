using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ServiceContracts.DTO.Cart;
using ServiceContracts.DTO.Feature;
using ServiceContracts.DTO.Image;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Product
{
    /// <summary>
    /// DTO class to add a new product
    /// </summary>
    public class ProductAddRequest
    {
        public ProductAddRequest()
        {
            this.Features = new HashSet<FeatureAddRequest>();
            this.Images = new HashSet<ImageAddRequest>();
        }
        public decimal Price { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public bool IsVisible { get; set; }
        public int ProductTypeId { get; set; }
        public ICollection<FeatureAddRequest> Features { get; set; }
        public ICollection<ImageAddRequest> Images { get; set; }

        public Entities.Models.Product ToProduct()
        {
            var product = new Entities.Models.Product()
            {
                Price = this.Price,
                Name = this.Name,
                Description = this.Description,
                Quantity = this.Quantity,
                IsVisible = this.IsVisible,
                ProductTypeId = this.ProductTypeId,
            };
            foreach (var feature in this.Features)
            {
                product.Features.Add(feature.ToFeature());
            }
            foreach (var image in this.Images)
            {
                product.Images.Add(image.ToProductImage());
            }
            return product;
        }
    }
}
