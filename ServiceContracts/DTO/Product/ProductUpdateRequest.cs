using Entities.Enums;
using Entities.Models;
using ServiceContracts.DTO.Feature;
using ServiceContracts.DTO.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Product
{
    public class ProductUpdateRequest
    {
        public ProductUpdateRequest(int id)
        {
            this.Features = new HashSet<FeatureResponse>();
            this.Images = new HashSet<ImageResponse>();
            Id = id;
        }
        public int Id { get; private set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public bool IsVisible { get; set; }
        public int ProductTypeId { get; set; }
        public ICollection<FeatureResponse> Features { get; set; }
        public ICollection<ImageResponse> Images { get; set; }

        public Entities.Models.Product ToProduct()
        {
            return new Entities.Models.Product()
            {
                Id = Id,
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
