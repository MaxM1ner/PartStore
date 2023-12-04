using DataAccess;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.ProductType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public sealed class ProductTypeManager : IProductTypeService
    {
        private readonly ApplicationDbContext _context;

        public ProductTypeManager(ApplicationDbContext context) => _context = context;
        public async Task<bool> IsExistAsync(int id)
        {
            return await _context.ProductTypes.AnyAsync(e => e.Id == id);
        }
        public async Task<ProductTypeResponse> GetProductTypeAsync(int id, bool includeProducts = true, bool includeFeatures = true)
        {
            var context = _context.ProductTypes.Where(x => x.Id == id);
            if (includeProducts)
                await context.Include(x => x.Products).ToListAsync();
            if (includeFeatures)
                await context.Include(x => x.Features).ToListAsync();
            var dbType = await context.FirstOrDefaultAsync() ?? throw new ArgumentException($"Not possible to find a ProductType by id:{id}", nameof(id));         
            return dbType.ToProductTypeResponse();
        }
        public async Task<List<ProductTypeResponse>> GetProductTypesAsync(bool includeProducts = true, bool includeFeatures = true)
        {
            var context = _context.ProductTypes;
            if (includeProducts)
                await context.Include(x => x.Products).ToListAsync();
            if (includeFeatures)
                await context.Include(x => x.Features).ToListAsync();
            return (await context.ToListAsync()).Select(x => x.ToProductTypeResponse()).ToList();
        }
        public async Task CreateAsync(ProductTypeAddRequest type)
        {
            await _context.ProductTypes.AddAsync(type.ToProductType());
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ProductTypeUpdateRequest type)
        {
            _context.ProductTypes.Update(type.ToProduct());
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(ProductTypeResponse type)
        {
            _context.ProductTypes.Remove(await _context.ProductTypes.FindAsync(type.Id));
            await _context.SaveChangesAsync();
        }
        public async Task DeleteByIdAsync(int id)
        {
            var feature = await GetProductTypeAsync(id);
            await DeleteAsync(feature);
        }
    }
}
