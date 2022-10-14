using PYP_Book.Application.Books.Queries.GetBook;
using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Languages.Queries.GetLanguage
{
    public class GetLanguageDto: IMapFrom<Language>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GetBookLanguageNestedDto> BookLanguages { get; set; }
    }
}
