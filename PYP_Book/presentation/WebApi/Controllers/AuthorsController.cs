using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PYP_Book.Application.Authors.Commands.CreateAuthor;
using PYP_Book.Application.Authors.Commands.DeleteAuthor;
using PYP_Book.Application.Authors.Commands.UpdateAuthor;
using PYP_Book.Application.Authors.Queries.GetAuthors;
using PYP_Book.Application.Authors.Queries.GetAuthor;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : BaseController
    {
        [HttpPost]
        public async Task<int> Post([FromForm] CreateAuthorCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPut]
        public async Task<Unit> Update([FromForm] UpdateAuthorCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet]
        public async Task<ICollection<GetAuthorsDto>> GetAll()
        {
            return await Mediator.Send(new GetAuthorsQuery());
        }
        [HttpGet("{id}")]
        public async Task<GetAuthorDto> Get(int id)
        {
            return await Mediator.Send(new GetAuthorQuery
            {
                Id = id
            });
        }
        [HttpDelete]
        public async Task Delete(DeleteAuthorCommand command)
        {
            await Mediator.Send(command);
        }
    }
}
