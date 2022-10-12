using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery : IRequest<IEnumerable<GetCategoryQuery>>;

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.AsNoTracking()
                 .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                 .OrderBy(x => x.CategoryName)
                 .ToListAsync(cancellationToken);

            return categories;
        }
    }

}
