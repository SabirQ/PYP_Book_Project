using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Categories.Queries.GetCategory
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryDto>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<GetCategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unit.CategoryRepository.GetByIdWithIncludesAsync(request.Id,nameof(Category.Books),nameof(Book.Discount));
            var categoryDto = _mapper.Map<GetCategoryDto>(entity);
            return categoryDto;
        }
    }
}
