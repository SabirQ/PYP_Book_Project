using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Categories.Queries.GetCategory
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryDto>
    {
        private readonly ICategoryRepository _repository;

        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetCategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdWithIncludesAsync(request.Id,nameof(Category.Books));
            var categoryDto = _mapper.Map<GetCategoryDto>(entity);
            return categoryDto;
        }
    }
}
