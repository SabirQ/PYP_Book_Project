using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;

namespace PYP_Book.Application.Discounts.Queries.GetDiscounts
{
    public record GetDiscountsQuery : IRequest<ICollection<GetDiscountsDto>>;

    public class GetDiscountsQueryHandler : IRequestHandler<GetDiscountsQuery, ICollection<GetDiscountsDto>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetDiscountsQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<ICollection<GetDiscountsDto>> Handle(GetDiscountsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unit.DiscountRepository.GetAllAsync();
            var DiscountsDto= _mapper.Map<ICollection<GetDiscountsDto>>(entities);
            return DiscountsDto;
        }
    }

}
