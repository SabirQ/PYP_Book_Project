using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PYP_Book.Application.Languages.Commands.CreateLanguage;
using PYP_Book.Application.Languages.Commands.DeleteLanguage;
using PYP_Book.Application.Languages.Commands.UpdateLanguage;
using PYP_Book.Application.Languages.Queries.GetLanguages;
using PYP_Book.Application.Languages.Queries.GetLanguage;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : BaseController
    {
        [HttpPost]
        public async Task<int> Post([FromForm] CreateLanguageCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPut]
        public async Task<Unit> Update([FromForm] UpdateLanguageCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet]
        public async Task<ICollection<GetLanguagesDto>> GetAll()
        {
            return await Mediator.Send(new GetLanguagesQuery());
        }
        [HttpGet("{id}")]
        public async Task<GetLanguageDto> Get(int id)
        {
            return await Mediator.Send(new GetLanguageQuery
            {
                Id = id
            });
        }
        [HttpDelete]
        public async Task Delete(DeleteLanguageCommand command)
        {
            await Mediator.Send(command);
        }
    }
}
