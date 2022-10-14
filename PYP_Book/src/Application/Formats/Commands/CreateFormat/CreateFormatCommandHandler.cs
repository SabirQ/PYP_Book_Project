using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Formats.Commands.CreateFormat
{
    public class CreateFormatCommandHandler : IRequestHandler<CreateFormatCommand, int>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public CreateFormatCommandHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateFormatCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Format>(request);
            await _unit.FormatRepository.AddAsync(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
