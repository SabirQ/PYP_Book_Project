using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Common.Interfaces.Services
{
    public interface IFileUploadService
    {
        Task<string> FileCreateAsync(IFormFile file);
        void FileDelete(string root, string folder, string image);

        bool CheckImage(IFormFile file, int mb);
    }
}
