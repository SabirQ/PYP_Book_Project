using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, int>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public CreateLanguageCommandHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Language>(request);
            await _unit.LanguageRepository.AddAsync(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
