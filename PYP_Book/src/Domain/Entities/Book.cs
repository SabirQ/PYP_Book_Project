using Domain.Commons;

namespace PYP_Book.Domain.Entities
{
    public class Book:BaseAuditableEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<BookFormat> BookFormats { get; set; }
        public ICollection<BookImage> BookImages { get; set; }
        public ICollection<BookLanguage> BookLanguages { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
