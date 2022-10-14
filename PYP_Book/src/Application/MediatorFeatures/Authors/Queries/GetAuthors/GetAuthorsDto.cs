using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;


namespace PYP_Book.Application.Authors.Queries.GetAuthors
{
    public class GetAuthorsDto:IMapFrom<Author>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
