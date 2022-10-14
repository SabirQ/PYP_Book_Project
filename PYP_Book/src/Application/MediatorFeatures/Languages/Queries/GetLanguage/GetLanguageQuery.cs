using MediatR;

namespace PYP_Book.Application.Languages.Queries.GetLanguage
{
    public record GetLanguageQuery : IRequest<GetLanguageDto>
    {
        public int Id { get; set; }
    };
}
