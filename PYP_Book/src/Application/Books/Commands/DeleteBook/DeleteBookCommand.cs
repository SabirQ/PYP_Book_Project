using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Books.Commands.DeleteBook
{
    public record DeleteBookCommand(int Id) : IRequest;
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _repository;

        public DeleteBookCommandHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            Book entity = await _repository.GetByIdWithIncludesAsync(request.Id,nameof(Book.Books),nameof(Book.Book));
            if (entity == null)
            {
                //throw new NotFoundException(nameof(DeleteBookCommand), request.Id);
                throw new ArgumentException();
            }
            if (entity.Books!=null)
            {
                for (int i = 0; i < entity.Books.Count; i++)
                {
                    entity.Books.ElementAt(i).BookId = null;
                }
            }
            await _repository.SoftDeleteAsync(entity, cancellationToken);
            return Unit.Value;
        }

    }
}
