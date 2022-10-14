using MediatR;
using Microsoft.AspNetCore.Http;
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
        public int? AuthorId { get; set; }
        public int? DiscountId { get; set; }
        public int? CategoryId { get; set; }
        //public ICollection<BookFormat> BookFormats { get; set; }
        public ICollection<CreateBookImageNestedCommand> Images { get; set; }
        //public ICollection<BookLanguage> BookLanguages { get; set; }
        //public ICollection<Comment> Comments { get; set; }
    }
}
