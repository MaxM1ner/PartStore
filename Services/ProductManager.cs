﻿using Microsoft.EntityFrameworkCore;
using DataAccess;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Services
{
    public sealed class ProductManager
    {
        private readonly ApplicationDbContext _context;

        public ProductManager(ApplicationDbContext context) => _context = context;
        public async Task<bool> IsExistAsync(int id)
        {
            return await _context.Products.AnyAsync(e => e.Id == id);
        }
        public async Task<Product> GetProductAsync(int id)
        {
            return await _context.Products.Where(x => x.Id == id).Include(x => x.Comments).Include(x => x.Features).Include(x => x.Images).Include(x => x.Type).FirstOrDefaultAsync() ?? throw new ArgumentException($"Not possible to find a product by id:{id}", nameof(id));
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(x => x.Comments).Include(x => x.Features).Include(x => x.Images).Include(x => x.Type).ToListAsync();
        }
        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var product = await GetProductAsync(id);
            await DeleteAsync(product);
        }
    }
}
