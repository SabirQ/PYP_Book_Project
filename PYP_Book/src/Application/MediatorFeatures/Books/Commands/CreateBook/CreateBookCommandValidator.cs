using FluentValidation;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        private const int ACCEPTABLE_FILE_SIZE = 2;
        private const int MINIMAL_PRICE = 1;
        private const int MAX_PRICE = 1000;
        private readonly IUnitOfWork _unit;

        public CreateBookCommandValidator(IUnitOfWork unit)
        {
            _unit = unit;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters")
                .MustAsync(IsUniqueName).WithMessage("The specified name already exists");
            RuleFor(x => x.Images)
                .NotEmpty().WithMessage("You must choose at least one image for primary")
                .Must(CheckFileSizeAndType).WithMessage($"Primary image was not choosen or file size(must be less or equal to {ACCEPTABLE_FILE_SIZE}) and file type is not supported");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(MINIMAL_PRICE)
                .WithMessage($"Book price must be more than or equal to {MINIMAL_PRICE}")
                .LessThanOrEqualTo(MAX_PRICE)
                .WithMessage($"Book price must be less than or equal to {MAX_PRICE}");
            RuleFor(x => x.DiscountId)
              .MustAsync(IsDiscountExist).WithMessage($"Discount was not found");
            RuleFor(x => x.AuthorId)
               .NotEmpty().WithMessage("AuthorId required")
               .MustAsync(IsAuthorExist).WithMessage($"Author was not found");
            RuleFor(x => x.CategoryId)
              .NotEmpty().WithMessage("CategoryId required")
              .MustAsync(IsCategoryExist).WithMessage($"Category was not found");
            RuleFor(x => x.LanguageIds)
                .Must(CheckCount).WithMessage("You have to add atleast one Language Id");
            RuleFor(x => x.FormatIds)
                .Must(CheckCount).WithMessage("You have to add atleast one Format Id");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");
        }

        public async Task<bool> IsUniqueName(string bookName, CancellationToken cancellationToken)
        {
            var book=await _unit.BookRepository.GetAllAsync(x=>x.Name==bookName,true);
            return book.Count==0;
        }
        public bool CheckFileSizeAndType(ICollection<IFormFile> files)
        {
            if (files == null) return false;
            if (files.Count==0) return false;
            if (!_unit.FileUpload.CheckImage(files.ElementAt(0), ACCEPTABLE_FILE_SIZE))
            {
                return false;
            }
            return true;
        }
        public async Task<bool> IsAuthorExist(int? id, CancellationToken cancellationToken)
        {
            if (id == null) return false;
            var author = await _unit.AuthorRepository.GetByIdAsync(id.GetValueOrDefault());
            if (author == null) return false;
            return true;
        }
        public async Task<bool> IsCategoryExist(int? id, CancellationToken cancellationToken)
        {
            if (id == null) return false;
            var category = await _unit.CategoryRepository.GetByIdAsync(id.GetValueOrDefault());
            if (category == null) return false;
            return true;
        }
        public async Task<bool> IsDiscountExist(int? id, CancellationToken cancellationToken)
        {
            if (id == null) return true;
            var discount = await _unit.DiscountRepository.GetByIdAsync(id.GetValueOrDefault());
            if (discount == null) return false;
            return true;
        }
        public bool CheckCount(ICollection<int> ids)
        {
            if (ids==null) return false;
            if (ids.Count > 0) return true;
            return false;
        }
    }
}
