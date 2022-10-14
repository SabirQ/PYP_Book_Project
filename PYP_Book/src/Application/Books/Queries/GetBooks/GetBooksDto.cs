using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Books.Queries.GetBooks
{
    public class GetBooksDto:IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
