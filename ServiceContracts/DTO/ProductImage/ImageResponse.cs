using Entities.Enums;
using Entities.Models;
using ServiceContracts.DTO.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Image
{
    /// <summary>
    /// DTO class used as return type of ImageManager methods
    /// </summary>
    public class ImageResponse
    {
        public int Id { get; set; }
        public string Path { get; set; } = null!;
        public int ProductId { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(ImageResponse)) return false;

            ImageResponse? toCompare = obj as ImageResponse;
            if (toCompare == null) return false;

            return toCompare.Id == Id &&
                toCompare.Path == Path &&
                toCompare.ProductId == ProductId;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string? ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public static class ProductImageExtensions
    {
        public static ImageResponse ToImageResponse(this ProductImage productImage)
        {
            return new ImageResponse
            {
                Id = productImage.Id,
                Path = productImage.Path,
                ProductId = productImage.ProductId
            };
        }
    }
}
