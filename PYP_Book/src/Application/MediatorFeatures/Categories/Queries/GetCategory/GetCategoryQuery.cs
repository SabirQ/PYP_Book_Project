using MediatR;

namespace PYP_Book.Application.Categories.Queries.GetCategory
{
    public record GetCategoryQuery : IRequest<GetCategoryDto>
    {
        public int Id { get; set; }
    };
}
