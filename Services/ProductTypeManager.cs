using DataAccess;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public sealed class ProductTypeManager
    {
        private readonly ApplicationDbContext _context;

        public ProductTypeManager(ApplicationDbContext context) => _context = context;
        public async Task<bool> IsExistAsync(int id)
        {
            return await _context.ProductTypes.AnyAsync(e => e.Id == id);
        }
        public async Task<ProductType> GetProductTypeAsync(int id, bool includeProducts = true, bool includeFeatures = true)
        {
            var context = _context.ProductTypes.Where(x => x.Id == id);
            if (includeProducts)
                await context.Include(x => x.Products).ToListAsync();
            if (includeFeatures)
                await context.Include(x => x.Features).ToListAsync();
            return await context.FirstOrDefaultAsync() ?? throw new ArgumentException($"Not possible to find a ProductType by id:{id}", nameof(id));
        }
        public async Task<List<ProductType>> GetProductTypesAsync(bool includeProducts = true, bool includeFeatures = true)
        {
            var context = _context.ProductTypes;
            if (includeProducts)
                await context.Include(x => x.Products).ToListAsync();
            if (includeFeatures)
                await context.Include(x => x.Features).ToListAsync();
            return await context.ToListAsync();
        }
        public async Task CreateAsync(ProductType feature)
        {
            await _context.ProductTypes.AddAsync(feature);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ProductType feature)
        {
            _context.ProductTypes.Update(feature);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(ProductType feature)
        {
            _context.ProductTypes.Remove(feature);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var feature = await GetProductTypeAsync(id);
            await DeleteAsync(feature);
        }
    }
}
