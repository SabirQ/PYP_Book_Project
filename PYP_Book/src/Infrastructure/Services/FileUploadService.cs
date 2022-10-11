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
        public FileUploadService(IWebHostEnvironment env)
        {

        }
        public bool CheckImage(IFormFile file, int mb)
        {
            return file.Length / 1024 / 1024 < mb && file.ContentType.Contains("image/");
        }

        public async Task<string> FileCreate(IFormFile file, string root, string folder)
        {
            string Name = file.FileName;
            string path = Path.Combine(root, folder);
            string Location = Path.Combine(path, Name);

            using (FileStream stream = new FileStream(Location, FileMode.Create))
            {
               await file.CopyToAsync(stream);
            }
            return Location;
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
