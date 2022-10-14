using FluentValidation;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Formats.Commands.CreateFormat
{
    public class CreateFormatCommandValidator : AbstractValidator<CreateFormatCommand>
    {
        private readonly IUnitOfWork _unit;

        public CreateFormatCommandValidator(IUnitOfWork unit)
        {
            _unit = unit;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is requried")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters")
                .MustAsync(IsUniqueName).WithMessage("The specified name already exists");
            
        }

        public async Task<bool> IsUniqueName(string FormatName, CancellationToken cancellationToken)
        {
            var Format=await _unit.FormatRepository.GetAllAsync(x=>x.Name==FormatName,true);
            return Format.Count==0;
        }
    }
}
