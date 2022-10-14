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
        public ICategoryRepository CategoryRepository { get; }
        public IDiscountRepository DiscountRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public IFileUploadService FileUpload { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
