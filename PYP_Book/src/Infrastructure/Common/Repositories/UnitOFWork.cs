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
        private readonly ILanguageRepository _languageRepository;
        private readonly IFormatRepository _formatRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ISettingRepository _settingRepository;

        private readonly IFileUploadService _fileUpload;
        private readonly AppDbContext _context;

        public IBookRepository BookRepository => _bookRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IDiscountRepository DiscountRepository => _discountRepository;
        public IAuthorRepository AuthorRepository => _authorRepository;
        public ILanguageRepository LanguageRepository => _languageRepository;
        public IFormatRepository  FormatRepository => _formatRepository;
        public ICommentRepository  CommentRepository => _commentRepository;
        public ISettingRepository  SettingRepository => _settingRepository;

        public IFileUploadService FileUpload => _fileUpload;

        public UnitOFWork(IBookRepository bookRepository
            ,IAuthorRepository authorRepository
            ,ICategoryRepository categoryRepository
            ,IDiscountRepository discountRepository
            ,ILanguageRepository languageRepository
            ,IFormatRepository formatRepository
            ,ICommentRepository commentRepository
            ,ISettingRepository settingRepository
            , IFileUploadService fileUpload
            ,AppDbContext context)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _discountRepository = discountRepository;
            _languageRepository = languageRepository;
            _formatRepository = formatRepository;
            _commentRepository = commentRepository;
            _settingRepository = settingRepository;
            _fileUpload = fileUpload;
            _context = context;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
