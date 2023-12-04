using DataAccess;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO;
using ServiceContracts.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Services
{
    public class ProductImageManager : IProductImageService
    {
        private readonly ApplicationDbContext _context;
        public ProductImageManager(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<ImageResponse> CreateAsync(ImageAddRequest image)
        {
            var productImage = image.ToProductImage();
            await _context.ProductImages.AddAsync(productImage);
            await _context.SaveChangesAsync();
            return productImage.ToImageResponse();
        }

        public async Task DeleteAsync(ImageResponse image)
        {
            await DeleteAsync(image.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var productImage = await _context.ProductImages.Where(x => x.Id == id).FirstAsync();
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
        }

        public async Task<ImageResponse> EditAsync(ImageUpdateRequest image)
        {
            var productImage = image.ToProductImage();
            _context.ProductImages.Update(productImage);
            await _context.SaveChangesAsync();
            return productImage.ToImageResponse();
        }

        public async Task<ImageResponse> GetImageAsync(int id)
        {
            var productImage = await _context.ProductImages.Where(x => x.Id == id).FirstAsync();
            return productImage.ToImageResponse();
        }

        public async Task<List<ImageResponse>> GetImagesAsync()
        {
            var productImages = await _context.ProductImages.Select(x => x.ToImageResponse()).ToListAsync();
            return productImages;
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _context.ProductImages.AnyAsync(x => x.Id == id);
        }
    }
}
