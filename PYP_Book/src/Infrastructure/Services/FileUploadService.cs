using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Infrastructure.Services
{
    public class FileUploadService : IFileUploadService
    {
        public bool CheckImage(IFormFile file, int mb)
        {
            return file.Length / 1024 / 1024 < mb && file.ContentType.Contains("image/");
        }

        public async Task<string> FileCreateAsync(IFormFile file)
        {
            string fileName = string.Concat(Guid.NewGuid(), file.FileName);
            var myAccount = new Account { ApiKey = "469283345716785", ApiSecret = "9kFDoqVLKMN264VuWl19NFKnNYw", Cloud = "dbueffn2s" };
            var _cloudinary = new Cloudinary(myAccount);
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, stream),
                };
                uploadResult =await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult.SecureUri.ToString();
        }

        public void FileDelete(string root, string folder, string image)
        {
            string filePath = Path.Combine(root, folder, image);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
