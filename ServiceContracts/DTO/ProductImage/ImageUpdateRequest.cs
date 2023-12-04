using Entities.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Image
{
    public class ImageUpdateRequest
    {
        public ImageUpdateRequest(string newPath, int newProductId)
        {
            Path = newPath; ProductId = newProductId;
        }
        public string Path { get; set; }
        public int ProductId { get; set; }

        public ProductImage ToProductImage()
        {
            return new ProductImage()
            {
                Path = this.Path,
                ProductId = this.ProductId
            };
        }
    }
}
