using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Formats.Queries.GetFormat;
using PYP_Book.Application.Languages.Queries.GetLanguage;
using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<int>, IMapFrom<Book>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }=0;
        public int? AuthorId { get; set; } = null;
        public int? DiscountId { get; set; } = null;
        public int? CategoryId { get; set; } = null;
        public ICollection<GetBookFormatNestedDto>? BookFormats { get; set; }
        public ICollection<IFormFile> Images { get; set; }
        public ICollection<GetBookLanguageNestedDto> BookLanguages { get; set; }
        //public ICollection<Comment> Comments { get; set; }
    }
}
