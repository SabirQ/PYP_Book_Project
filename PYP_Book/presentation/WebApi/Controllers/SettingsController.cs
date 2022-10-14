//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using PYP_Book.Application.Settings.Commands.CreateSetting;
//using PYP_Book.Application.Settings.Commands.DeleteSetting;
//using PYP_Book.Application.Settings.Commands.UpdateSetting;
//using PYP_Book.Application.Settings.Queries.GetSettings;
//using PYP_Book.Application.Settings.Queries.GetSetting;

//namespace WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SettingsController : BaseController
//    {
//        [HttpPost]
//        public async Task<int> Post([FromForm] CreateSettingCommand command)
//        {
//            return await Mediator.Send(command);
//        }
//        [HttpPut]
//        public async Task<Unit> Update([FromForm] UpdateSettingCommand command)
//        {
//            return await Mediator.Send(command);
//        }
//        [HttpGet]
//        public async Task<ICollection<GetSettingsDto>> GetAll()
//        {
//            return await Mediator.Send(new GetSettingsQuery());
//        }
//        [HttpGet("{id}")]
//        public async Task<GetSettingDto> Get(int id)
//        {
//            return await Mediator.Send(new GetSettingQuery
//            {
//                Id = id
//            });
//        }
//        [HttpDelete]
//        public async Task Delete(DeleteSettingCommand command)
//        {
//            await Mediator.Send(command);
//        }
//    }
//}
