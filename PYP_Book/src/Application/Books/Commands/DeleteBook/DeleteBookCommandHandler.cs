using MediatR;
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
            Book entity = await _unit.BookRepository.GetByIdWithIncludesAsync(request.Id,nameof(Book.BookImages));
            if (entity == null)
            {
                //throw new NotFoundException(nameof(DeleteBookCommand), request.Id);
                throw new ArgumentException();
            }
            if (entity.BookImages !=null)
            {
                for (int i = 0; i < entity.BookImages.Count; i++)
                {
                    entity.BookImages.ElementAt(i).BookId = null;
                    entity.BookImages.ElementAt(i).Deleted = true;
                }
            }
            _unit.BookRepository.SoftDelete(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
