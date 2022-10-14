using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PYP_Book.Application.Formats.Commands.CreateFormat;
using PYP_Book.Application.Formats.Commands.DeleteFormat;
using PYP_Book.Application.Formats.Commands.UpdateFormat;
using PYP_Book.Application.Formats.Queries.GetFormats;
using PYP_Book.Application.Formats.Queries.GetFormat;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormatsController : BaseController
    {
        [HttpPost]
        public async Task<int> Post([FromForm] CreateFormatCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPut]
        public async Task<Unit> Update([FromForm] UpdateFormatCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet]
        public async Task<ICollection<GetFormatsDto>> GetAll()
        {
            return await Mediator.Send(new GetFormatsQuery());
        }
        [HttpGet("{id}")]
        public async Task<GetFormatDto> Get(int id)
        {
            return await Mediator.Send(new GetFormatQuery
            {
                Id = id
            });
        }
        [HttpDelete]
        public async Task Delete(DeleteFormatCommand command)
        {
            await Mediator.Send(command);
        }
    }
}
