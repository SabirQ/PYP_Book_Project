using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;


namespace PYP_Book.Application.Discounts.Queries.GetDiscounts
{
    public class GetDiscountsDto:IMapFrom<Discount>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Percentage { get; set; }
    }
}
