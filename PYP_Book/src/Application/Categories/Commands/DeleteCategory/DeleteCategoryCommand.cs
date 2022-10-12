using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(int Id) : IRequest;
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _repository;

        public DeleteCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Category entity = await _repository.GetByIdWithIncludesAsync(request.Id,"Books","Book.Category");
            if (entity == null)
            {
                //throw new NotFoundException(nameof(DeleteCategoryCommand), request.Id);
                throw new ArgumentException();
            }
            if (entity.Books!=null)
            {
                foreach (Book book in entity.Books)
                {
                    book.CategoryId = null;
                    book.Category = null;
                }
                //soft delete ask
            }

            await _repository.SoftDeleteAsync(entity, cancellationToken);
            return Unit.Value;
        }

    }
}
