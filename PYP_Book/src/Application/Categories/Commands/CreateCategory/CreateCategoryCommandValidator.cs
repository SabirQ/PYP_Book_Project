using FluentValidation;
using Microsoft.AspNetCore.Http;
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
        private readonly IFileUploadService _fileUpload;
        private const int ACCEPTABLE_FILE_SIZE = 2;

        public CreateCategoryCommandValidator(ICategoryRepository repository,IFileUploadService fileUpload)
        {
            _repository = repository;
            _fileUpload = fileUpload;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is requried")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters")
                .MustAsync(IsUniqueName).WithMessage("The specified name already exists");
            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Image was not Choosen")
                .Must(CheckFileSizeAndType).WithMessage("File size or file type is not supported");
        }

        public async Task<bool> IsUniqueName(string categoryName, CancellationToken cancellationToken)
        {
            var category=await _repository.GetAllAsync(x=>x.Name==categoryName,true);
            return category==null;
        }
        public bool CheckFileSizeAndType(IFormFile file)
        {
            return _fileUpload.CheckImage(file, ACCEPTABLE_FILE_SIZE);
        }
    }
}
