using Entities.Models;
using Microsoft.EntityFrameworkCore;
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
        /// Upload image from HttpRequest on local directory
        /// </summary>
        /// <param name="imageFile">IFormFile from that sends with HttpRequest</param>
        /// <returns>New image file name</returns>
        public Task<bool> IsExistAsync(int id);
        /// <summary>
        /// Upload image from HttpRequest on local directory
        /// </summary>
        /// <param name="imageFile">IFormFile from that sends with HttpRequest</param>
        /// <returns>New image file name</returns>
        public Task<FeatureResponse> GetFeatureAsync(int id);
        /// <summary>
        /// Upload image from HttpRequest on local directory
        /// </summary>
        /// <param name="imageFile">IFormFile from that sends with HttpRequest</param>
        /// <returns>New image file name</returns>
        public Task<List<FeatureResponse>> GetFeaturesAsync();
        /// <summary>
        /// Upload image from HttpRequest on local directory
        /// </summary>
        /// <param name="imageFile">IFormFile from that sends with HttpRequest</param>
        /// <returns>New image file name</returns>
        public Task<FeatureResponse> CreateAsync(FeatureAddRequest feature);
        /// <summary>
        /// Upload image from HttpRequest on local directory
        /// </summary>
        /// <param name="imageFile">IFormFile from that sends with HttpRequest</param>
        /// <returns>New image file name</returns>
        public Task<FeatureResponse> EditAsync(FeatureUpdateRequest feature);
        /// <summary>
        /// Upload image from HttpRequest on local directory
        /// </summary>
        /// <param name="imageFile">IFormFile from that sends with HttpRequest</param>
        /// <returns>New image file name</returns>
        public Task DeleteAsync(FeatureResponse feature);
        /// <summary>
        /// Upload image from HttpRequest on local directory
        /// </summary>
        /// <param name="imageFile">IFormFile from that sends with HttpRequest</param>
        /// <returns>New image file name</returns>
        public Task DeleteAsync(int id);
    }
}
