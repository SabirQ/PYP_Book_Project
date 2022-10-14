using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Languages.Queries.GetLanguage
{
    public class GetBookLanguageNestedDto : IMapFrom<BookLanguage>
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int BookId { get; set; }
    }
}
