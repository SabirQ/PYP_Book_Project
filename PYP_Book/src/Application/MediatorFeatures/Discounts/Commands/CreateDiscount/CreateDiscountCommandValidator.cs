using FluentValidation;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Interfaces;

namespace PYP_Book.Application.Discounts.Commands.CreateDiscount
{
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {

        public CreateDiscountCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is requried")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters")
                .MinimumLength(3).WithMessage("Name must not be less than 3 characters");
            RuleFor(x => x.Percentage)
               .NotEmpty().WithMessage("Percentage is required")
               .GreaterThanOrEqualTo((byte)1)
               .WithMessage("Percentage must be greater or equal to 1")
               .LessThanOrEqualTo((byte)100)
               .WithMessage("Percentage must be less or equal to 100");
        }
    }
}
