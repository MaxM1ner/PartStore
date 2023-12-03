using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceContracts
{
    public interface IFormImageService
    {
        /// <summary>
        /// Upload image from HttpRequest on local directory
        /// </summary>
        /// <param name="imageFile">IFormFile from that sends with HttpRequest</param>
        /// <returns>New image file name</returns>
        public Task<string> UploadImage(IFormFile imageFile);
        /// <summary>
        /// Delete image from local directory
        /// </summary>
        /// <param name="fileName">File name to delete</param>
        public void DeleteImage(string fileName);
        /// <param name="imageName">Product comment to add</param>
        /// <returns>Full path of image file</returns>
        public string GetImagePath(string imageName);
    }
}
