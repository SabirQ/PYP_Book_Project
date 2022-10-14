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
        private readonly IUnitOfWork _unit;

        public DeleteCategoryCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Category entity = await _unit.CategoryRepository.GetByIdWithIncludesAsync(request.Id,nameof(Category.Books),nameof(Book.Category));
            if (entity == null)
            {
                //throw new NotFoundException(nameof(DeleteCategoryCommand), request.Id);
                throw new ArgumentException();
            }
            if (entity.Books!=null)
            {
                for (int i = 0; i < entity.Books.Count; i++)
                {
                    entity.Books.ElementAt(i).CategoryId = null;
                }
            }
            _unit.CategoryRepository.SoftDelete(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
