using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Categories.Commands.CreateCategory
{
    //public class CreateCategoryCommand : IRequest<int>, IMapFrom<Category>
    //{
    //    public string CategoryName { get; set; } = null!;
    //    public string? Description { get; set; }
    //}

    //public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    //{
    //    private readonly IApplicationDbContext _context;
    //    private readonly IMapper _mapper;
    //    public CreateCategoryCommandHandler(IApplicationDbContext context, IMapper mapper)
    //    {
    //        this._context = context;
    //        this._mapper = mapper;
    //    }

    //    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    //    {

    //        var entity = _mapper.Map<Category>(request);
    //        //var entity1 = new Category
    //        //{
    //        //    CategoryName = request.CategoryName,
    //        //    Description = request.Description
    //        //};

    //        _context.Categories.Add(entity);
    //        await _context.SaveChangesAsync(cancellationToken);
    //        return entity.Id;
    //    }
    //}
}
