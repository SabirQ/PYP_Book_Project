using MediatR;
using PYP_Book.Application.Common.Exceptions;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteBookCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            Book entity = await _unit.BookRepository
                .GetByIdWithIncludesAsync(
                request.Id
                ,nameof(Book.BookImages)
                ,nameof(Book.BookFormats)
                ,nameof(Book.BookLanguages)
                );

            if (entity == null)
            {
                throw new NotFoundException(nameof(DeleteBookCommand), request.Id);
            }
            entity.AuthorId = null;
            entity.CategoryId=null;
            entity.DiscountId = null;
            if (entity.BookImages !=null)
            {
                for (int i = 0; i < entity.BookImages.Count; i++)
                {
                    entity.BookImages.ElementAt(i).BookId = null;
                    entity.BookImages.ElementAt(i).Deleted = true;
                }
            }
            if (entity.BookLanguages != null)
            {
                for (int i = 0; i < entity.BookLanguages.Count; i++)
                {
                    entity.BookLanguages.ElementAt(i).BookId = null;
                    entity.BookLanguages.ElementAt(i).Deleted = true;
                    entity.BookLanguages.ElementAt(i).LanguageId = null;
                }
            }
            if (entity.BookFormats != null)
            {
                for (int i = 0; i < entity.BookFormats.Count; i++)
                {
                    entity.BookFormats.ElementAt(i).BookId = null;
                    entity.BookFormats.ElementAt(i).Deleted = true;
                    entity.BookFormats.ElementAt(i).FormatId = null;
                }
            }
            _unit.BookRepository.SoftDelete(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
