using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Books.Commands.CreateBook;
using PYP_Book.Application.Common.Exceptions;
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
            if (entity == null) throw new NotFoundException(nameof(UpdateBookCommand), request.Id);
            entity.AuthorId = request.AuthorId;
            entity.CategoryId = request.CategoryId;
            entity.DiscountId=request.DiscountId;

            if(!string.IsNullOrEmpty(entity.Name)) entity.Name = request.Name;
            if(entity.Price!=null) entity.Price = request.Price.Value;
            if(entity.Stock!=null) entity.Stock = request.Stock.Value;

            entity = await DeleteImagesAsync(entity, request);
            _unit.BookRepository.Update(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        public async Task<Book> DeleteImagesAsync(Book entity, UpdateBookCommand request)
        {
            if (request.SetFirstImageAsPrimary == true)
            {
                if (request.Images.Count == 0 || !_unit.FileUpload.CheckImage(request.Images.ElementAt(0), ACCEPTABLE_FILE_SIZE))
                    throw new PrimaryDeleteException("Primary image is not valid or Image was not choosen");
                
                var existedPrimary = entity.BookImages.FirstOrDefault(x => x.Primary == true);
                existedPrimary.Deleted = true;
                existedPrimary.BookId = null;
            }
            else
            {
                var primary = entity.BookImages.FirstOrDefault(x => x.Primary == true);
                var deletePrimary = request.BookImagesDeleteIds.FirstOrDefault(x => x == primary.Id);
                if (deletePrimary != null)
                    throw new PrimaryDeleteException($"Tried delete primary BookImage{primary.Id} image without adding antoher primary BookImage");
            }
            if (request.BookImagesDeleteIds != null)
            {
                for (int i = 0; i < request.BookImagesDeleteIds.Count; i++)
                {
                    var delete = entity.BookImages.FirstOrDefault(x => x.Id == request.BookImagesDeleteIds.ElementAt(i));
                    if (delete != null)
                    {
                        delete.Deleted = true;
                        delete.BookId = null;
                    }
                }
            }
            if (request.Images!=null) entity = await MapImagesAsync(entity, request.Images, request.SetFirstImageAsPrimary);
            return entity;
        }
        public async Task<Book> MapImagesAsync(Book book, ICollection<IFormFile> images,bool setPrimary)
        {
            for (int i = 0; i < images.Count; i++)
            {
                if (_unit.FileUpload.CheckImage(images.ElementAt(i), ACCEPTABLE_FILE_SIZE))
                {
                    BookImage image = new BookImage
                    {
                        Alternative = images.ElementAt(i).FileName,
                        ImageUrl = await _unit.FileUpload.FileCreateAsync(images.ElementAt(i)),
                        Book = book,
                        Primary= setPrimary
                    };
                    book.BookImages.Add(image);
                }
                setPrimary = false;
            }
            return book;
        }
    }
}
