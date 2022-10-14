using MediatR;

namespace PYP_Book.Application.Authors.Commands.DeleteAuthor
{
    public record DeleteAuthorCommand(int Id) : IRequest;
}
