using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Books.Queries.GetBook
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, GetBookDto>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetBookQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<GetBookDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unit.BookRepository.GetByIdWithIncludesAsync(request.Id,nameof(Book.Books),nameof(Book.Discount));
            var BookDto = _mapper.Map<GetBookDto>(entity);
            return BookDto;
        }
    }
}
