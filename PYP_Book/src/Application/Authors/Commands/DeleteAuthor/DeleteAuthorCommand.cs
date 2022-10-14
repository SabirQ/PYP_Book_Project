using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Authors.Commands.DeleteAuthor
{
    public record DeleteAuthorCommand(int Id) : IRequest;
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteAuthorCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            Author entity = await _unit.AuthorRepository.GetByIdWithIncludesAsync(request.Id,nameof(Author.Books));
            if (entity == null)
            {
                //throw new NotFoundException(nameof(DeleteAuthorCommand), request.Id);
                throw new ArgumentException();
            }
            if (entity.Books!=null)
            {
                for (int i = 0; i < entity.Books.Count; i++)
                {
                    entity.Books.ElementAt(i).AuthorId = null;
                }
            }
            _unit.AuthorRepository.SoftDelete(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
