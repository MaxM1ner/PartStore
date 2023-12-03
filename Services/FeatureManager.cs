using DataAccess;
using DataAccess.Migrations;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public sealed class FeatureManager : IFeatureService
    {
        private readonly ApplicationDbContext _context;

        public FeatureManager(ApplicationDbContext context, IProductService productManager) 
        {
            _context = context;
        }


        public async Task<FeatureResponse> CreateAsync(FeatureAddRequest feature)
        {
            var newFeature = feature.ToFeature();
            await _context.Features.AddAsync(newFeature);
            await _context.SaveChangesAsync();
            return newFeature.ToFeatureResponse();
        }

        public async Task DeleteAsync(FeatureResponse feature)
        {
            await DeleteAsync(feature.FeatureId);
        }

        public async Task DeleteAsync(int id)
        {
            var dbFeature = await _context.Features.Where(x => x.Id == id).FirstAsync();
            _context.Features.Remove(dbFeature);
            await _context.SaveChangesAsync();
        }

        public async Task<FeatureResponse> EditAsync(FeatureUpdateRequest feature)
        {
            var featureToUpdate = _context.Features.Where(x => x.Id == feature.FeatureId).First();
            featureToUpdate.Value = feature.Value;
            featureToUpdate.ProductTypeId = feature.ProductTypeId;
            featureToUpdate.Name = feature.Name;

            var dbProducts = new List<Product>();
            foreach (var productId in feature.ProductIds) 
            { 
                var product = await _context.Products.Where(x => x.Id == productId).FirstAsync();
                featureToUpdate.Products.Add(product); 
            }
            _context.Features.Update(featureToUpdate);
            await _context.SaveChangesAsync();
            return featureToUpdate.ToFeatureResponse();
        }

        public async Task<FeatureResponse> GetFeatureAsync(int id)
        {
            var dbFeature = await _context.Features.Where(x => x.Id == id).FirstAsync();
            return dbFeature.ToFeatureResponse();
        }

        public async Task<List<FeatureResponse>> GetFeaturesAsync()
        {
            var dbFeatures = _context.Features;
            var featuresResponse = await dbFeatures.Select(x => x.ToFeatureResponse()).ToListAsync();
            return featuresResponse;
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _context.Features.AnyAsync(e => e.Id == id);
        }
        
    }
}
