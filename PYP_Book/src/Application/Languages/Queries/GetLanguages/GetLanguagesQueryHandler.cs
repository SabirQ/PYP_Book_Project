using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;

namespace PYP_Book.Application.Languages.Queries.GetLanguages
{
    public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, ICollection<GetLanguagesDto>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetLanguagesQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ICollection<GetLanguagesDto>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unit.LanguageRepository.GetAllAsync();
            var languagesDto= _mapper.Map<ICollection<GetLanguagesDto>>(entities);
            return languagesDto;
        }
    }

}
