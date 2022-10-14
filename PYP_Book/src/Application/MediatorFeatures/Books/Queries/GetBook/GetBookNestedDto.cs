
using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Books.Queries.GetBook
{
    public class GetBookNestedDto:IMapFrom<Book>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? DiscountId { get; set; }
        public GetDiscountNestedDto Discount { get; set; }
        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }
        public ICollection<GetBookImageNestedDto> BookImages { get; set; }
    }
    public class GetDiscountNestedDto:IMapFrom<Discount>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Percentage { get; set; }
    }
}
