using PYP_Book.Application.Common.Interfaces.Repositories;
using PYP_Book.Domain.Entities;
using PYP_Book.Infrastructure.Common.Repositories.GenericRepository;
using PYP_Book.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Infrastructure.Common.Repositories
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        public DiscountRepository(AppDbContext context) : base(context)
        {
        }
    }
}
