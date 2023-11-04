using StoreUI.Data;
using StoreUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public sealed class ProductManager
    {
        private readonly ApplicationDbContext _context;

        public ProductManager(ApplicationDbContext context) => _context = context;

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id) ?? throw new ArgumentException($"Not possible to find a product by id:{id}");
        }
        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }
        public async Task Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task Edit(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var product = await GetProduct(id);
            await Delete(product);
        }
    }
}
