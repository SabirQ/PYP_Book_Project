using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;

namespace PYP_Book.Application.Books.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, ICollection<GetBooksDto>>
    {
        private readonly IBookRepository _repository;

        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetBooksDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            var BooksDto= _mapper.Map<ICollection<GetBooksDto>>(entities);
            return BooksDto;
        }
    }

}
