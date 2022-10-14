using PYP_Book.Application.Authors.Queries.GetAuthor;
using PYP_Book.Application.Books.Queries.GetBook;
using PYP_Book.Application.Categories.Queries.GetCategory;
using PYP_Book.Application.Formats.Queries.GetFormat;
using PYP_Book.Application.Languages.Queries.GetLanguage;
using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Books.Queries.GetBook
{
    public class GetBookDto: IMapFrom<Book>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public int? AuthorId { get; set; }
        public GetAuthorNestedDto? Author { get; set; }
        public int? DiscountId { get; set; }
        public GetDiscountNestedDto? Discount { get; set; }
        public int? CategoryId { get; set; }
        public GetCategoryNestedDto? Category { get; set; }
        public ICollection<GetBookFormatNestedDto> BookFormats { get; set; }
        public ICollection<GetBookImageNestedDto> BookImages { get; set; }
        public ICollection<GetBookLanguageNestedDto> BookLanguages { get; set; }
        //public ICollection<Comment> Comments { get; set; }

    }
}
