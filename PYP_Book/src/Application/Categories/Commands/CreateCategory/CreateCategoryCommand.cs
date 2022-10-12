using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>, IMapFrom<Category>
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
