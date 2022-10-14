using MediatR;

namespace PYP_Book.Application.Discounts.Queries.GetDiscount
{
    public record GetDiscountQuery : IRequest<GetDiscountDto>
    {
        public int Id { get; set; }
    };
}
