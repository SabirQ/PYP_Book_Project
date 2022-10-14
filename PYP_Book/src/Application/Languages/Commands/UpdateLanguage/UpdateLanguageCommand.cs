using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Exceptions;
using PYP_Book.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand>
    {
        private readonly IUnitOfWork _unit;

        public UpdateLanguageCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unit.LanguageRepository.GetByIdAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(UpdateLanguageCommand), request.Id);
            
            entity.Name = request.Name;
            _unit.LanguageRepository.Update(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
