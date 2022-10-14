using MediatR;

namespace PYP_Book.Application.Books.Queries.GetBook
{
    public record GetBookQuery : IRequest<GetBookDto>
    {
        public int Id { get; set; }
    };
}
