using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.Feature;
using ServiceContracts.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IFeatureService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> IsExistAsync(int id);
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<FeatureResponse> GetFeatureAsync(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<FeatureResponse>> GetFeaturesAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public Task<FeatureResponse> CreateAsync(FeatureAddRequest feature);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public Task<FeatureResponse> EditAsync(FeatureUpdateRequest feature);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public Task DeleteAsync(FeatureResponse feature);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteAsync(int id);
    }
}
