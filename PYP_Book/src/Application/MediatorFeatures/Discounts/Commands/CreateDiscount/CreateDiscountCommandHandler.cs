using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Discounts.Commands.CreateDiscount
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, int>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CreateDiscountCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Discount>(request);
            await _unit.DiscountRepository.AddAsync(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
