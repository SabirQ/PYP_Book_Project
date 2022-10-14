using MediatR;
using PYP_Book.Application.Common.Exceptions;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Formats.Commands.DeleteFormat
{
    public class DeleteFormatCommandHandler : IRequestHandler<DeleteFormatCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteFormatCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(DeleteFormatCommand request, CancellationToken cancellationToken)
        {
            Format entity = await _unit.FormatRepository.GetByIdWithIncludesAsync(request.Id,nameof(Format.BookFormats));
            if (entity == null)
            {
                throw new NotFoundException(nameof(DeleteFormatCommand), request.Id);
            }
            if (entity.BookFormats!=null)
            {
                for (int i = 0; i < entity.BookFormats.Count; i++)
                {
                    entity.BookFormats.ElementAt(i).FormatId = null;
                    entity.BookFormats.ElementAt(i).BookId = null;
                    entity.BookFormats.ElementAt(i).Deleted = true;
                }
            }
            _unit.FormatRepository.SoftDelete(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
