using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Discounts.Commands.DeleteDiscount
{
    public record DeleteDiscountCommand(int Id) : IRequest;
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteDiscountCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount entity = await _unit.DiscountRepository.GetByIdWithIncludesAsync(request.Id,nameof(Discount.Books),nameof(Book.Discount));
            if (entity == null)
            {
                //throw new NotFoundException(nameof(DeleteDiscountCommand), request.Id);
                throw new ArgumentException();
            }
            if (entity.Books!=null)
            {
                for (int i = 0; i < entity.Books.Count; i++)
                {
                    entity.Books.ElementAt(i).DiscountId = null;
                }
            }
            _unit.DiscountRepository.SoftDelete(entity);
            await _unit.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
