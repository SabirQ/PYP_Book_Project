using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Formats.Queries.GetFormat
{
    public class GetFormatQueryHandler : IRequestHandler<GetFormatQuery, GetFormatDto>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetFormatQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<GetFormatDto> Handle(GetFormatQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unit.FormatRepository.GetByIdWithIncludesAsync(request.Id,nameof(Format.BookFormats));
            var FormatDto = _mapper.Map<GetFormatDto>(entity);
            return FormatDto;
        }
    }
}
