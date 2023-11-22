using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FormImageManager
    {
        //enum Directories{
        //    ProductImageDirectory,
        //    ProductTypeDirectory
        //}
        private readonly string _filesDirectory;
        public FormImageManager(string filesDirectory)
        {
            if (filesDirectory is null || filesDirectory == string.Empty) throw new ArgumentNullException(nameof(filesDirectory), "filesDirectory can not be null or empty.");
            _filesDirectory = filesDirectory;
            if (!Directory.Exists(_filesDirectory))
            {
                Directory.CreateDirectory(_filesDirectory);
            }
        }
        public async Task<string> UploadImage(IFormFile imageFile)
        {      
            if (imageFile is null || imageFile.Length < 0) throw new ArgumentNullException(nameof(imageFile), "You can not upload an empty image.");
            string uniqueFileName = (Guid.NewGuid().ToString() + "_" + imageFile.FileName).Replace(' ', '_');
            using (var stream = new FileStream(_filesDirectory + uniqueFileName, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            return uniqueFileName;
        }
        public void DeleteImage(string fileName)
        {
            var filePath = Path.Combine(_filesDirectory, fileName);
            if (fileName is null || fileName == string.Empty) throw new ArgumentNullException(nameof(fileName), "fileName can not be null or empty.");
            if(File.Exists(filePath)) File.Delete(filePath); else throw new ArgumentException($"Can not find the file on path {filePath}.");
        }
        public string GetImagePath(string imageName)
        {
            return Path.Combine(_filesDirectory, imageName);
        }
    }
}
