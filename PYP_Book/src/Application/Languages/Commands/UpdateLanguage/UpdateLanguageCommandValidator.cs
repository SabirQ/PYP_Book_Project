using FluentValidation;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommandValidator : AbstractValidator<UpdateLanguageCommand>
    {
        private readonly IUnitOfWork _unit;

        public UpdateLanguageCommandValidator(IUnitOfWork unit)
        {
            _unit = unit;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is requried")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters")
                .MustAsync(IsUniqueName).WithMessage("The specified name already exists");
        }
        public async Task<bool> IsUniqueName(string name, CancellationToken cancellationToken)
        {
            var Language = await _unit.LanguageRepository.GetAllAsync(x => x.Name == name, true);
            return Language.Count == 0;
        }
    }
}
