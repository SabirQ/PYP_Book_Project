using MediatR;

namespace PYP_Book.Application.Discounts.Commands.DeleteDiscount
{
    public record DeleteDiscountCommand(int Id) : IRequest;
}
