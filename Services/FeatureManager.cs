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
    public sealed class FeatureManager
    {
        private readonly ApplicationDbContext _context;

        public FeatureManager(ApplicationDbContext context) => _context = context;
        public async Task<bool> IsExistAsync(int id)
        {
            return await _context.Features.AnyAsync(e => e.Id == id);
        }
        public async Task<Feature> GetFeatureAsync(int id)
        {
            return await _context.Features.Include(x => x.Products).Include(x => x.Type).Where(x => x.Id == id).FirstOrDefaultAsync() ?? throw new ArgumentException($"Not possible to find a feature by id:{id}", nameof(id));
        }
        public async Task<List<Feature>> GetFeaturesAsync()
        {
            //Removed include methods
            return await _context.Features.ToListAsync();
        }
        public async Task CreateAsync(Feature feature)
        {
            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(Feature feature)
        {
            _context.Features.Update(feature);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Feature feature)
        {
            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var feature = await GetFeatureAsync(id);
            await DeleteAsync(feature);
        }
    }
}
