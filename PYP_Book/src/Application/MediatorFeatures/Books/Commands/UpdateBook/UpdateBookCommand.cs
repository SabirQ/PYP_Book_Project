using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Books.Commands.CreateBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public int? AuthorId { get; set; }
        public int? DiscountId { get; set; }
        public int? CategoryId { get; set; }
        public ICollection<int> FormatIds { get; set; }
        public ICollection<int>? BookImagesDeleteIds { get; set; }
        public ICollection<IFormFile>? Images { get; set; }
        public bool SetFirstImageAsPrimary { get; set; }=false;
        public ICollection<int> LanguageIds { get; set; }
        //public ICollection<Comment> Comments { get; set; }
    }
}
