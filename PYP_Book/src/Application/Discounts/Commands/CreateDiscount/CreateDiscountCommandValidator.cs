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
            RuleFor(x => x.Surname)
               .NotEmpty().WithMessage("Surname is requried")
               .MaximumLength(50).WithMessage("Surname must not exceed 50 characters")
               .MinimumLength(3).WithMessage("Surname must not be less than 3 characters");
        }
    }
}
