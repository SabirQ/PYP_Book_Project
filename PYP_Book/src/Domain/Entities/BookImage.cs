using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Domain.Entities
{
    public class BookImage: BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Alternative { get; set; }
        public int? BookId { get; set; }
        public Book? Book { get; set; }
    }
}
