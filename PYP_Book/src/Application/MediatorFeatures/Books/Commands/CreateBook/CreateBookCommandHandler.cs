using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Exceptions;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private const int ACCEPTABLE_FILE_SIZE = 2;

        public CreateBookCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        
        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Book>(request);
            if (request.Images!=null)
            {
                entity.BookImages=await MapImagesAsync(entity,request.Images);
            }
            entity =await CreateToManyRelationship(entity, request);
            await _unit.BookRepository.AddAsync(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
        public async Task<Book> CreateToManyRelationship(Book book, CreateBookCommand request)
        {
            book.BookLanguages = new List<BookLanguage>();
            ICollection<Language> languages=await _unit.LanguageRepository.GetAllAsync();
            for (int i = 0; i < request.LanguageIds.Count; i++)
            {
                Language language = languages.FirstOrDefault(x => x.Id == request.LanguageIds.ElementAt(i));
                if (language==null) throw new NotFoundException($"Language with {i} Id was not found");
                BookLanguage bookLanguage=new BookLanguage()
                {
                    Book=book,
                    Language=language
                };
                book.BookLanguages.Add(bookLanguage);
            }
            book.BookFormats = new List<BookFormat>();
            ICollection<Format> formats = await _unit.FormatRepository.GetAllAsync();
            for (int i = 0; i < request.FormatIds.Count; i++)
            {
                Format format = formats.FirstOrDefault(x => x.Id == request.FormatIds.ElementAt(i));
                if (format == null) throw new NotFoundException($"Format with {i} Id was not found");
                BookFormat bookFormat = new BookFormat()
                {
                    Book = book,
                    Format = format
                };
                book.BookFormats.Add(bookFormat);
            }
            return book;
        }
        public async Task<ICollection<BookImage>> MapImagesAsync(Book book, ICollection<IFormFile> images)
        {
            ICollection<BookImage> bookImages = new List<BookImage>();
            for (int i = 0; i < images.Count; i++)
            {
                if (!_unit.FileUpload.CheckImage(images.ElementAt(i), ACCEPTABLE_FILE_SIZE))
                {
                    images.Remove(images.ElementAt(i));
                }
                BookImage image = new BookImage
                {
                    Alternative = images.ElementAt(i).FileName,
                    ImageUrl = await _unit.FileUpload.FileCreateAsync(images.ElementAt(i)),
                    Book = book,
                    Primary=(i==0?true:false)
                };
                bookImages.Add(image);
            }
            return bookImages;
        }
    }
}
