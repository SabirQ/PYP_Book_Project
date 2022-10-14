using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Languages.Queries.GetLanguage
{
    public class GetLanguageQueryHandler : IRequestHandler<GetLanguageQuery, GetLanguageDto>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetLanguageQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<GetLanguageDto> Handle(GetLanguageQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unit.LanguageRepository.GetByIdWithIncludesAsync(request.Id,nameof(Language.BookLanguages));
            var languageDto = _mapper.Map<GetLanguageDto>(entity);
            return languageDto;
        }
    }
}
