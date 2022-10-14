using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Exceptions;
using PYP_Book.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Formats.Commands.UpdateFormat
{
    public class UpdateFormatCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateFormatCommandHandler : IRequestHandler<UpdateFormatCommand>
    {
        private readonly IUnitOfWork _unit;

        public UpdateFormatCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(UpdateFormatCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unit.FormatRepository.GetByIdAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(UpdateFormatCommand), request.Id);
            
            entity.Name = request.Name;
            _unit.FormatRepository.Update(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
