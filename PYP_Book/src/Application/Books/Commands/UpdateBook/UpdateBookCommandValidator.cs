using FluentValidation;
using Microsoft.AspNetCore.Http;
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
        private readonly IBookRepository _repository;
        private readonly IFileUploadService _fileUpload;
        private const int ACCEPTABLE_FILE_SIZE = 2;

        public UpdateBookCommandValidator(IBookRepository repository,IFileUploadService fileUpload)
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
        public async Task<bool> IsUniqueName(UpdateBookCommand model, string name, CancellationToken cancellationToken)
        {
            var book = await _repository.GetAllAsync(x => x.Name == name, true);
            return book.Count == 0;
        }
        public bool CheckFileSizeAndType(IFormFile file)
        {
            if (file!=null) return true;
            return _fileUpload.CheckImage(file, ACCEPTABLE_FILE_SIZE);
        }
    }
}
