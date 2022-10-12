using FluentValidation;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _repository;

        public CreateCategoryCommandValidator(ICategoryRepository repository)
        {
            _repository = repository;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is requried")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters")
                .MustAsync(IsUniqeName).WithMessage("The specified name already exists");
            //TODO check photo
        }

        public async Task<bool> IsUniqeName(string categoryName, CancellationToken cancellationToken)
        {
            var category=await _repository.GetAllAsync(x=>x.Name==categoryName);
            return category==null;
        }
    }
}
