using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Domain.Entities
{
    public class Category: BaseAuditableEntity
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
