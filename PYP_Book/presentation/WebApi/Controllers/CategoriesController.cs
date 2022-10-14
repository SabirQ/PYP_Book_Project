using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PYP_Book.Application.Categories.Commands.CreateCategory;
using PYP_Book.Application.Categories.Commands.DeleteCategory;
using PYP_Book.Application.Categories.Commands.UpdateCategory;
using PYP_Book.Application.Categories.Queries.GetCategories;
using PYP_Book.Application.Categories.Queries.GetCategory;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        [HttpPost]
        public async Task<int> Post([FromForm] CreateCategoryCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPut]
        public async Task<Unit> Update([FromForm] UpdateCategoryCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet]
        public async Task<ICollection<GetCategoriesDto>> GetAll()
        {
            return await Mediator.Send(new GetCategoriesQuery());
        }
        [HttpGet("{id}")]
        public async Task<GetCategoryDto> Get(int id)
        {
            return await Mediator.Send(new GetCategoryQuery
            {
                Id = id
            });
        }
        [HttpDelete]
        public async Task Delete(DeleteCategoryCommand command)
        {
            await Mediator.Send(command);
        }
    }
}
