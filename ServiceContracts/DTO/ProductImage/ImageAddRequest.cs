using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ServiceContracts.DTO.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Order
{
    /// <summary>
    /// DTO class to add a new image
    /// </summary>
    public sealed class ImageAddRequest
    {
        public ImageAddRequest(string path, int productId)
        {
            Path = path; ProductId = productId;
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
