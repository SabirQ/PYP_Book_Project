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
        private const int ACCEPTABLE_FILE_SIZE = 2;
        private const int MINIMAL_PRICE = 1;
        private const int MAX_PRICE = 1;

        public UpdateBookCommandValidator( IUnitOfWork unit)
        {
            _unit = unit;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters")
                .MustAsync(IsUniqueName).WithMessage("The specified name already exists");
            RuleFor(x => x.Images)
                .Must(CheckFileSizeAndType).WithMessage($"Primary image was not choosen or file size(must be less or equal to {ACCEPTABLE_FILE_SIZE}) and file type is not supported");
            RuleFor(x => x.Price)
                .LessThanOrEqualTo(MINIMAL_PRICE)
                .WithMessage($"Book price must be more than or equal to {MINIMAL_PRICE}")
                .GreaterThanOrEqualTo(MAX_PRICE)
                .WithMessage($"Book price must be less than or equal to {MAX_PRICE}");

        }
        public async Task<bool> IsUniqueName(string bookName, CancellationToken cancellationToken)
        {
            var book = await _unit.BookRepository.GetAllAsync(x => x.Name == bookName, true);
            return book.Count == 0;
        }
        public bool CheckFileSizeAndType(ICollection<CreateBookImageNestedCommand> files)
        {
            bool isPrimaryValid = false;
            int PrimaryCount = 0;
            for (int i = 0; i < files.Count; i++)
            {
                if (files.ElementAt(i).Primary == true)
                {
                    PrimaryCount++;
                    isPrimaryValid = _unit.FileUpload.CheckImage(files.ElementAt(i).Image, ACCEPTABLE_FILE_SIZE);
                }
            }
            return isPrimaryValid && PrimaryCount == 1;
        }
    }
}
