using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CreateAuthorCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Author>(request);
            await _unit.AuthorRepository.AddAsync(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
