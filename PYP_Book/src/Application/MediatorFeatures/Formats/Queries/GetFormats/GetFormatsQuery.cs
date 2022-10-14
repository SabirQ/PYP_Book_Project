using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Formats.Queries.GetFormats
{
    public record GetFormatsQuery : IRequest<ICollection<GetFormatsDto>>;

}
