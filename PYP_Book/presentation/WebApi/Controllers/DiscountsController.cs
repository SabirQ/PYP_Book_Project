using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PYP_Book.Application.Discounts.Commands.CreateDiscount;
using PYP_Book.Application.Discounts.Commands.DeleteDiscount;
using PYP_Book.Application.Discounts.Commands.UpdateDiscount;
using PYP_Book.Application.Discounts.Queries.GetDiscounts;
using PYP_Book.Application.Discounts.Queries.GetDiscount;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : BaseController
    {
        [HttpPost]
        public async Task<int> Post([FromForm] CreateDiscountCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPut]
        public async Task<Unit> Update([FromForm] UpdateDiscountCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet]
        public async Task<ICollection<GetDiscountsDto>> GetAll()
        {
            return await Mediator.Send(new GetDiscountsQuery());
        }
        [HttpGet("{id}")]
        public async Task<GetDiscountDto> Get(int id)
        {
            return await Mediator.Send(new GetDiscountQuery
            {
                Id = id
            });
        }
        [HttpDelete]
        public async Task Delete(DeleteDiscountCommand command)
        {
            await Mediator.Send(command);
        }
    }
}
