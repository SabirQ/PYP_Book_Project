using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;

namespace PYP_Book.Application.Formats.Queries.GetFormats
{
    public class GetFormatsQueryHandler : IRequestHandler<GetFormatsQuery, ICollection<GetFormatsDto>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetFormatsQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ICollection<GetFormatsDto>> Handle(GetFormatsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unit.FormatRepository.GetAllAsync();
            var FormatsDto= _mapper.Map<ICollection<GetFormatsDto>>(entities);
            return FormatsDto;
        }
    }

}
