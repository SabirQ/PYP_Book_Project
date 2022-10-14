using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Infrastructure.Common.Repositories
{
    public class UnitOFWork : IUnitOfWork
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IFileUploadService _fileUpload;
        private readonly AppDbContext _context;

        public IBookRepository BookRepository => _bookRepository;

        public ICategoryRepository CategoryRepository => _categoryRepository;

        public IDiscountRepository DiscountRepository => _discountRepository;

        public IAuthorRepository AuthorRepository => _authorRepository;

        public IFileUploadService FileUpload => _fileUpload;
        public UnitOFWork(IBookRepository bookRepository
            ,IAuthorRepository authorRepository
            ,ICategoryRepository categoryRepository
            ,IDiscountRepository discountRepository
            ,IFileUploadService fileUpload
            ,AppDbContext context)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _discountRepository = discountRepository;
            _fileUpload = fileUpload;
            _context = context;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
