using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.Cart;
using ServiceContracts.DTO.Order;

namespace ServiceContracts
{
    /// <summary>
    /// Represents product type service methods to use product types
    /// </summary>
    public interface IProductTypeService
    {
        public Task<bool> IsExistAsync(int id);
        public Task<ProductTypeResponse> GetProductTypeAsync(int id, bool includeProducts = true, bool includeFeatures = true);
        public Task<List<ProductTypeResponse>> GetProductTypesAsync(bool includeProducts = true, bool includeFeatures = true);
        public Task CreateAsync(ProductTypeAddRequest feature);
        public Task UpdateAsync(ProductTypeUpdateRequest feature);
        public Task DeleteAsync(ProductTypeResponse feature);
        public Task DeleteByIdAsync(int id);
    }
}