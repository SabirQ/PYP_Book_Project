using PYP_Book.Application.Books.Queries.GetBook;
using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Authors.Queries.GetAuthor
{
    public class GetAuthorDto: IMapFrom<Author>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<GetBookNestedDto> Books { get; set; }
    }
}
