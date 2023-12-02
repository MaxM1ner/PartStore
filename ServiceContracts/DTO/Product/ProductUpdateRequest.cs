using Entities.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Order
{
    public class ProductUpdateRequest
    {
        public ProductUpdateRequest()
        {
            this.Features = new HashSet<Feature>();
            this.Images = new HashSet<ProductImage>();
        }
        public decimal Price { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public bool IsVisible { get; set; }
        public int ProductTypeId { get; set; }
        public ICollection<Feature> Features { get; set; }
        public ICollection<ProductImage> Images { get; set; }

        public Product ToProduct()
        {
            return new Product()
            {
                Price = this.Price,
                Name = this.Name,
                Description = this.Description,
                Quantity = this.Quantity,
                IsVisible = this.IsVisible,
                ProductTypeId = this.ProductTypeId
            };
        }
    }
}
