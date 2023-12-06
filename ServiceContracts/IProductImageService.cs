using ServiceContracts.DTO.Image;
using ServiceContracts.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public interface IProductImageService
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
        public Task<ImageResponse> GetImageAsync(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<ImageResponse>> GetImagesAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Task<ImageResponse> CreateAsync(ImageAddRequest image);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Task<ImageResponse> EditAsync(ImageUpdateRequest image);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image "></param>
        /// <returns></returns>
        public Task DeleteAsync(ImageResponse image);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteAsync(int id);
    }
}
