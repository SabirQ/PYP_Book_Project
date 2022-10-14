using PYP_Book.Application.Common.Interfaces.Repositories;
using PYP_Book.Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        public IBookRepository BookRepository { get; }
        public ILanguageRepository LanguageRepository { get; }
        public IFormatRepository FormatRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public ISettingRepository SettingRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IDiscountRepository DiscountRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public IFileUploadService FileUpload { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
