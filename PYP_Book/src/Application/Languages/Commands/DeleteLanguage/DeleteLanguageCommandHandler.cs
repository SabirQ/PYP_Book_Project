using MediatR;
using PYP_Book.Application.Common.Exceptions;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteLanguageCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            Language entity = await _unit.LanguageRepository.GetByIdWithIncludesAsync(request.Id,nameof(Language.BookLanguages));
            if (entity == null)
            {
                throw new NotFoundException(nameof(DeleteLanguageCommand), request.Id);
            }
            if (entity.BookLanguages!=null)
            {
                for (int i = 0; i < entity.BookLanguages.Count; i++)
                {
                    entity.BookLanguages.ElementAt(i).LanguageId = null;
                    entity.BookLanguages.ElementAt(i).BookId = null;
                    entity.BookLanguages.ElementAt(i).Deleted = true;
                }
            }
            _unit.LanguageRepository.SoftDelete(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
