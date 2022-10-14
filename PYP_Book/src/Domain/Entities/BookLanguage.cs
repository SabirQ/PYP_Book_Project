using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Domain.Entities
{
    public class BookLanguage: BaseAuditableEntity
    {
        public int? BookId { get; set; }
        public Book Book { get; set; }
        public int? LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
