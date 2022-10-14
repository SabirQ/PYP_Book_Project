using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Books.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, ICollection<GetBooksDto>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ICollection<GetBooksDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unit.BookRepository.GetAllAsync(null,false,nameof(Book.Author),nameof(Book.BookImages), nameof(Book.Discount));
            var BooksDto= _mapper.Map<ICollection<GetBooksDto>>(entities);
            return BooksDto;
        }
    }

}
