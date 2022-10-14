using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Books.Queries.GetBooks
{
    public record GetBooksQuery : IRequest<ICollection<GetBooksDto>>;

}
