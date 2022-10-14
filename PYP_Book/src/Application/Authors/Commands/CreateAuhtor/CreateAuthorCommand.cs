using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<int>, IMapFrom<Author>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
