using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Common.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> expression, params string[] includes);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task SoftDeleteAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
