using AutoMapper;
using MediatR;
using PYP_Book.Application.Books.Commands.CreateBook;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private const int ACCEPTABLE_FILE_SIZE = 2;

        public UpdateBookCommandHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unit.BookRepository.GetByIdWithIncludesAsync(request.Id,nameof(Book.BookImages));
            if (entity == null) throw new ArgumentException();
           //throw new NotFoundException("UpdateBookCommand");
           //throw new NotFoundException(nameof(DeleteBookCommand), request.Id);
            entity.AuthorId = request.AuthorId;
            entity.CategoryId = request.CategoryId;
            entity.DiscountId=request.DiscountId;

            if(!string.IsNullOrEmpty(entity.Name)) entity.Name = request.Name;
            if(entity.Price!=null) entity.Price = request.Price.Value;
            if(entity.Stock!=null) entity.Stock = request.Stock.Value;
            if (request.BookImagesDeleteIds != null)
            {
                var primary= entity.BookImages.FirstOrDefault(x => x.Primary == true);
                var primaryUpdate=request.Images.FirstOrDefault(x => x.Primary == true);
                bool primaryDeleteExist = false;
                for (int i = 0; i < request.BookImagesDeleteIds.Count; i++)
                {
                    if (request.BookImagesDeleteIds.ElementAt(i) == primary.Id)
                    {
                        if (primaryUpdate!=null)
                        {
                            primary.Deleted = true;
                            primary.Primary = false;
                            var deletedPrimary = primary;
                            entity.BookImages.Remove(primary);
                            entity.BookImages.Add(deletedPrimary);
                        }
                        throw new Exception();
                    }
                    for (int j = 0; j < entity.BookImages.Count; j++)
                    {
                        if (request.BookImagesDeleteIds.ElementAt(i)== entity.BookImages.ElementAt(j).Id&& !entity.BookImages.ElementAt(j).Primary)
                        {
                            entity.BookImages.ElementAt(j).BookId = null;
                            entity.BookImages.ElementAt(j).Deleted = false;
                            entity.BookImages.Remove(entity.BookImages.ElementAt(j));
                        }
                    }
                }
                entity = await MapImagesAsync(entity, request.Images);
            }
            _unit.BookRepository.Update(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        public async Task<Book> MapImagesAsync(Book book, ICollection<CreateBookImageNestedCommand> images)
        {
            
            
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
                book.BookImages.Add(image);
            }
            return book;
        }
    }
}
