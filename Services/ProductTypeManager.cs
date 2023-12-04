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
        public async Task<ProductTypeResponse> GetProductTypeAsync(int id)
        {
            var context = _context.ProductTypes.Include(x => x.Products).Include(x => x.Features).Where(x => x.Id == id);
            var dbType = await context.FirstOrDefaultAsync() ?? throw new ArgumentException($"Not possible to find a ProductType by id:{id}", nameof(id));         
            return dbType.ToProductTypeResponse();
        }
        public async Task<List<ProductTypeResponse>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.Include(x => x.Products).Include(x => x.Features).Select(x => x.ToProductTypeResponse()).ToListAsync();
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
