using FluentValidation;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _repository;
        private readonly IFileUploadService _fileUpload;
        private const int ACCEPTABLE_FILE_SIZE = 2;

        public UpdateCategoryCommandValidator(ICategoryRepository repository,IFileUploadService fileUpload)
        {
            _repository = repository;
            _fileUpload = fileUpload;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is requried")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters")
                .MustAsync(IsUniqueName).WithMessage("The specified name already exists");
            RuleFor(x => x.Image)
                .Must(CheckFileSizeAndType).WithMessage("File size or file type is not supported");
        }
        public async Task<bool> IsUniqueName(UpdateCategoryCommand model, string name, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAllAsync(x => x.Name == name, true);
            return category == null;
        }
        public bool CheckFileSizeAndType(IFormFile file)
        {
            if (file!=null) return true;
            return _fileUpload.CheckImage(file, ACCEPTABLE_FILE_SIZE);
        }
        public async Task<bool> BeUniqName(UpdateCategoryCommand model, CancellationToken cancellationToken)
        {
            if (model.Id == 0) return false;
            var entity = await _repository.GetAllAsync(x => x.Id != model.Id && x.Name == model.Name,true);
            if (entity==null)return true;
            return false;
        }
    }
}
