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
        //public bool IsExist(int id)
        //{
        //    return _context.ProductTypes.Any(e => e.Id == id);
        //}
        public async Task<ProductType> GetProductTypeAsync(int id)
        {
            return await _context.ProductTypes.Include(x => x.Products).Include(x => x.Features).Where(x => x.Id == id).FirstOrDefaultAsync() ?? throw new ArgumentException($"Not possible to find a ProductType by id:{id}", nameof(id));
        }
        public async Task<List<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.Include(x => x.Products).Include(x => x.Features).ToListAsync();
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
