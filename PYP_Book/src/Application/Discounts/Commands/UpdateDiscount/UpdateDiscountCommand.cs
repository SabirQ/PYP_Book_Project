using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Interfaces;

namespace PYP_Book.Application.Discounts.Commands.UpdateDiscount
{
    public class UpdateDiscountCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand>
    {
        private readonly IUnitOfWork _unit;

        public UpdateDiscountCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unit.DiscountRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new ArgumentException();
                //throw new NotFoundException("UpdateDiscountCommand");
                //throw new NotFoundException(nameof(DeleteDiscountCommand), request.Id);
            }

            entity.Name = request.Name;
            entity.Surname = request.Surname;
            _unit.DiscountRepository.Update(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
