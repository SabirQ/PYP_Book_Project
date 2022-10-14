using PYP_Book.Application.Books.Queries.GetBook;
using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Discounts.Queries.GetDiscount
{
    public class GetDiscountDto: IMapFrom<Discount>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Percentage { get; set; }
        public ICollection<GetBookNestedDto> Books { get; set; }
    }
}
