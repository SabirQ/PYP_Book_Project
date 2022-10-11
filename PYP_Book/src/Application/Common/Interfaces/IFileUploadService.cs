using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Common.Interfaces
{
    public interface IFileUploadService
    {
        Task<string> FileCreate(IFormFile file, string filename, string root, string folder);
        void FileDelete(string root, string folder, string image);

        bool CheckImage(IFormFile file, int mb);
    }
}
