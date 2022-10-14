using AutoMapper;
using MediatR;
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
                Discount discount = await _unit.DiscountRepository.GetByIdAsync(request.DiscountId.Value);
                if (discount==null)
                {
                    //Throw
                    throw new Exception();
                }
            }
            Author author = await _unit.AuthorRepository.GetByIdAsync(request.AuthorId.Value);
            if (author == null) throw new Exception();
            Category category = await _unit.CategoryRepository.GetByIdAsync(request.CategoryId.Value);
            if (category==null) throw new Exception();
            var entity = _mapper.Map<Book>(request);
            if (request.Images!=null)
            {
                entity.BookImages=await MapImagesAsync(entity,request.Images);
            }
            await _unit.BookRepository.AddAsync(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
        public async Task<ICollection<BookImage>> MapImagesAsync(Book book, ICollection<CreateBookImageNestedCommand> images)
        {
            ICollection<BookImage> bookImages = new List<BookImage>();
            for (int i = 0; i < images.Count; i++)
            {
                if (!_unit.FileUpload.CheckImage(images.ElementAt(i).Image, ACCEPTABLE_FILE_SIZE))
                {
                    images.Remove(images.ElementAt(i));
                }
                BookImage image = _mapper.Map<BookImage>(images.ElementAt(i));
                image.Alternative = images.ElementAt(i).Image.FileName;
                image.ImageUrl = await _unit.FileUpload.FileCreateAsync(images.ElementAt(i).Image);
                image.Book = book;
                bookImages.Add(image);
            }
            return bookImages;
        }
    }
}
