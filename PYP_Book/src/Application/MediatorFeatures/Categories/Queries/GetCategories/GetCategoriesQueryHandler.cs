using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;

namespace PYP_Book.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, ICollection<GetCategoriesDto>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ICollection<GetCategoriesDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unit.CategoryRepository.GetAllAsync();
            var categoriesDto= _mapper.Map<ICollection<GetCategoriesDto>>(entities);
            return categoriesDto;
        }
    }

}
