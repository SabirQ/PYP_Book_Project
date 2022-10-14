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
            if (request.DiscountId!=null)
            {
                Discount discount = await _unit.DiscountRepository.GetByIdAsync(request.DiscountId.GetValueOrDefault(0));
                if (discount==null)
                    throw new NotFoundException("Discount was not found");
            }
            Author author = await _unit.AuthorRepository.GetByIdAsync(request.AuthorId.GetValueOrDefault(0));
            if (author == null) throw new NotFoundException("Author was not found");
            Category category = await _unit.CategoryRepository.GetByIdAsync(request.CategoryId.GetValueOrDefault(0));
            if (category==null) throw new NotFoundException("Category was not found");
            var entity = _mapper.Map<Book>(request);
            if (request.Images!=null)
            {
                entity.BookImages=await MapImagesAsync(entity,request.Images);
            }
            await _unit.BookRepository.AddAsync(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return entity.Id;
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
