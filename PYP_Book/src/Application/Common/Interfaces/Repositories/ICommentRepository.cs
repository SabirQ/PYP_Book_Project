using PYP_Book.Application.Common.Interfaces.Repositories.GenericRepository;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Common.Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {

    }
}
