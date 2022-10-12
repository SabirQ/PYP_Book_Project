using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Categories.Commands.DeleteCategory
{
    //public record DeleteCategoryCommand(int Id) : IRequest;
    //public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    //{
    //    private readonly IApplicationDbContext _context;
    //    public DeleteCategoryCommandHandler(IApplicationDbContext context)
    //    {
    //        this._context = context;
    //    }

    //    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    //    {
    //        var entity = await _context.Categories.Where(x => x.Id == request.Id)
    //              .SingleOrDefaultAsync();

    //        if (entity == null)
    //        {
    //            throw new NotFoundException(nameof(DeleteCategoryCommand), request.Id);
    //        }

    //        _context.Categories.Remove(entity);
    //        await _context.SaveChangesAsync(cancellationToken);
    //        return Unit.Value;
    //    }

    //}
}
