using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Domain.Entities
{
    public class Discount: BaseAuditableEntity
    {
        public string Name { get; set; }
        public byte Percentage { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
