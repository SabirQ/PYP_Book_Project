using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Exceptions;
using PYP_Book.Application.Common.Interfaces;

namespace PYP_Book.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
   
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IUnitOfWork _unit;

        public UpdateAuthorCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unit.AuthorRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
               
                throw new NotFoundException(nameof(UpdateAuthorCommand), request.Id);
            }

            entity.Name = request.Name;
            entity.Surname = request.Surname;
            _unit.AuthorRepository.Update(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
