using MediatR;

namespace PYP_Book.Application.Authors.Queries.GetAuthor
{
    public record GetAuthorQuery : IRequest<GetAuthorDto>
    {
        public int Id { get; set; }
    };
}
