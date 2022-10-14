using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Exceptions;
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
            var entity = await _unit.BookRepository.GetByIdWithIncludesAsync(
                request.Id
                , nameof(Book.Category)
                , nameof(Book.Author)
                , nameof(Book.BookImages)
                , nameof(Book.BookFormats)
                , nameof(Book.BookLanguages)
                , nameof(Book.Discount));
            if (entity == null)
                throw new NotFoundException(nameof(Book), request.Id);
            var bookDto = _mapper.Map<GetBookDto>(entity);
            return bookDto;
        }
    }
}
