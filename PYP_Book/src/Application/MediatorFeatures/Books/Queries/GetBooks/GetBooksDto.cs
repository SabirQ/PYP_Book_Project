using PYP_Book.Application.Authors.Queries.GetAuthor;
using PYP_Book.Application.Books.Queries.GetBook;
using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Books.Queries.GetBooks
{
    public class GetBooksDto:IMapFrom<Book>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int? AuthorId { get; set; }
        public GetAuthorNestedDto? Author { get; set; }
        public int? DiscountId { get; set; }
        public GetDiscountNestedDto? Discount { get; set; }
        public ICollection<GetBookImageNestedDto> BookImages { get; set; }
    }
}
