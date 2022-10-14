using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Exceptions;
using PYP_Book.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IUnitOfWork _unit;

        public UpdateCategoryCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unit.CategoryRepository.GetByIdAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(UpdateCategoryCommand), request.Id);
           

            entity.Name = request.Name;
            if (request.Image!=null)
            {
               entity.ImageUrl=await _unit.FileUpload.FileCreateAsync(request.Image);
            }
            _unit.CategoryRepository.Update(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
