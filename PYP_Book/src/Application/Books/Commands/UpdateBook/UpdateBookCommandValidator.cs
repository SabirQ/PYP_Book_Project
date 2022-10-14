using FluentValidation;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Books.Commands.CreateBook;
using PYP_Book.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        private readonly IUnitOfWork _unit;
        private const int MINIMAL_PRICE = 1;
        private const int MAX_PRICE = 1000;

        public UpdateBookCommandValidator( IUnitOfWork unit)
        {
            _unit = unit;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters")
                .MustAsync(IsUniqueName).WithMessage("The specified name already exists");
            RuleFor(x => x.AuthorId)
                .MustAsync(IsAuthorExist).WithMessage($"Author was not found");
            RuleFor(x => x.DiscountId)
               .MustAsync(IsDiscountExist).WithMessage($"Discount was not found");
            RuleFor(x => x.CategoryId)
               .MustAsync(IsCategoryExist).WithMessage($"Category was not found");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(MINIMAL_PRICE)
                .WithMessage($"Book price must be more than or equal to {MINIMAL_PRICE}")
                .LessThanOrEqualTo(MAX_PRICE)
                .WithMessage($"Book price must be less than or equal to {MAX_PRICE}");

        }
        public async Task<bool> IsAuthorExist(int? id, CancellationToken cancellationToken)
        {
            if (id == null) return true;
            var author = await _unit.AuthorRepository.GetByIdAsync(id.Value);
            if (author == null) return false;
            return true;
        }
        public async Task<bool> IsCategoryExist(int? id, CancellationToken cancellationToken)
        {
            if (id == null) return true;
            var category = await _unit.CategoryRepository.GetByIdAsync(id.Value);
            if (category == null) return false;
            return true;
        }
        public async Task<bool> IsDiscountExist(int? id, CancellationToken cancellationToken)
        {
            if (id == null) return true;
            var discount = await _unit.DiscountRepository.GetByIdAsync(id.Value);
            if (discount == null) return false;
            return true;
        }
        public async Task<bool> IsUniqueName(string bookName,CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(bookName)) return true;
            var book = await _unit.BookRepository.GetAllAsync(x => x.Name == bookName, true);
            return book.Count == 0;
        }
    }
}
