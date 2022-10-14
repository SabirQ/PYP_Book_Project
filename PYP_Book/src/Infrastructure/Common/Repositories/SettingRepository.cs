using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;
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
