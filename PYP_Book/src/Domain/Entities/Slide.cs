using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Domain.Entities
{
    public class Slide: BaseAuditableEntity
    {
        public string Title { get; set; }
        public string SecondTitle { get; set; }
        public string Desciption { get; set; }
        public string ImageUrl { get; set; }
        public byte Order { get; set; }
    }
}
