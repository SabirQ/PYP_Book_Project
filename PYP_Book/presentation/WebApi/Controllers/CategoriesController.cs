using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PYP_Book.Application.Categories.Commands.CreateCategory;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        [HttpPost]
        public async Task<int> Post([FromBody] CreateCategoryCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
