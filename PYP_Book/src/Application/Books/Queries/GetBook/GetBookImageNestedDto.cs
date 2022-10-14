
using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Books.Queries.GetBook
{
    public class GetBookImageNestedDto : IMapFrom<BookImage>
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool Primary { get; set; }
    }
}
