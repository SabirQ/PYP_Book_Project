using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PYP_Book.Application.Books.Commands.CreateBook;
using PYP_Book.Application.Books.Commands.DeleteBook;
using PYP_Book.Application.Books.Commands.UpdateBook;
using PYP_Book.Application.Books.Queries.GetBooks;
using PYP_Book.Application.Books.Queries.GetBook;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseController
    {
        [HttpPost]
        public async Task<int> Post([FromForm] CreateBookCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPut]
        public async Task<Unit> Update([FromForm] UpdateBookCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet]
        public async Task<ICollection<GetBooksDto>> GetAll()
        {
            return await Mediator.Send(new GetBooksQuery());
        }
        [HttpGet("{id}")]
        public async Task<GetBookDto> Get(int id)
        {
            return await Mediator.Send(new GetBookQuery
            {
                Id = id
            });
        }
        [HttpDelete]
        public async Task Delete(DeleteBookCommand command)
        {
            await Mediator.Send(command);
        }
    }
}
