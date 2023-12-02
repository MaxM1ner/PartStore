using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.Cart;
using ServiceContracts.DTO.Order;

namespace ServiceContracts
{
    /// <summary>
    /// Represents product service methods to use products
    /// </summary>
    public interface IProductService
    {
        public Task<bool> IsExistAsync(int id);
        public Task<ProductResponse> GetProductAsync(int id, bool includeType = true, bool includeFeatures = true, bool includeImages = true, bool includeComments = true);
        public Task<List<ProductResponse>> GetProductsAsync(bool includeType = true, bool includeFeatures = true, bool includeImages = true, bool includeComments = true);
        public Task CreateAsync(ProductAddRequest product);
        public Task UpdateAsync(ProductUpdateRequest product);
        public Task DeleteAsync(ProductResponse product);
        public Task DeleteByIdAsync(int id);
    }
}