using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Formats.Commands.DeleteFormat
{
    public record DeleteFormatCommand(int Id) : IRequest;
}
