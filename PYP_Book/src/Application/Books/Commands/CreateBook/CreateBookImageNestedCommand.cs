using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Books.Commands.CreateBook
{
    public class CreateBookImageNestedCommand: IMapFrom<BookImage>
    {
        public IFormFile Image { get; set; }
        public bool Primary { get; set; }
    }
}
