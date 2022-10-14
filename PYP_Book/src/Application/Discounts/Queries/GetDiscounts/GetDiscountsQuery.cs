using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;

namespace PYP_Book.Application.Authors.Queries.GetAuthors
{
    public record GetAuthorsQuery : IRequest<ICollection<GetAuthorsDto>>;

    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, ICollection<GetAuthorsDto>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAuthorsQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<ICollection<GetAuthorsDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unit.AuthorRepository.GetAllAsync();
            var authorsDto= _mapper.Map<ICollection<GetAuthorsDto>>(entities);
            return authorsDto;
        }
    }

}
