using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Languages.Queries.GetLanguages
{
    public record GetLanguagesQuery : IRequest<ICollection<GetLanguagesDto>>;

}
