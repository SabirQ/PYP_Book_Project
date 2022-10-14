using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Authors.Queries.GetAuthor
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, GetAuthorDto>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAuthorQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<GetAuthorDto> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unit.AuthorRepository.GetByIdWithIncludesAsync(request.Id,nameof(Author.Books),"Books.Discount");
            var authorDto = _mapper.Map<GetAuthorDto>(entity);
            return authorDto;
        }
    }
}
