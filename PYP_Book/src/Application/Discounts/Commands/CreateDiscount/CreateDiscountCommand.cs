using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Discounts.Commands.CreateDiscount
{
    public class CreateDiscountCommand : IRequest<int>, IMapFrom<Discount>
    {
        public string Name { get; set; }
        public byte Percentage { get; set; }
    }
}
