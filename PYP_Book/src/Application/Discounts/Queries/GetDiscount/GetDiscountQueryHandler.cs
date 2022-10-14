using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Discounts.Queries.GetDiscount
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, GetDiscountDto>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetDiscountQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<GetDiscountDto> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unit.DiscountRepository.GetByIdWithIncludesAsync(request.Id,nameof(Discount.Books),"Books.Discount");
            var DiscountDto = _mapper.Map<GetDiscountDto>(entity);
            return DiscountDto;
        }
    }
}
