using MediatR;

namespace PYP_Book.Application.Formats.Queries.GetFormat
{
    public record GetFormatQuery : IRequest<GetFormatDto>
    {
        public int Id { get; set; }
    };
}
