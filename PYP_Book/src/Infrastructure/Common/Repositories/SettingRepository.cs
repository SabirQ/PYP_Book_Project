using PYP_Book.Application.Common.Interfaces.Repositories;
using PYP_Book.Domain.Entities;
using PYP_Book.Infrastructure.Common.Repositories.GenericRepository;
using PYP_Book.Infrastructure.Data;

namespace PYP_Book.Infrastructure.Common.Repositories
{
    public class SettingRepository : Repository<Setting>, ISettingRepository
    {
        public SettingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
