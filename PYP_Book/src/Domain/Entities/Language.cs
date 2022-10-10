using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Domain.Entities
{
    public class Language: BaseAuditableEntity
    {
        public string Name { get; set; }
        public ICollection<BookLanguage> BookLanguages { get; set; }
    }
}
