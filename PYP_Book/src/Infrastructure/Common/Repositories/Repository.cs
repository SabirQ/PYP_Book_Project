using Domain.Commons;
using Microsoft.EntityFrameworkCore;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Infrastructure.Common.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
           _dbSet.Remove(entity);
           await _context.SaveChangesAsync();
        }

        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> expression=null, params string[] includes)
        {
            IQueryable<T> query = expression is null ? _dbSet.AsQueryable() : _dbSet.Where(expression);
            query = query.Where(x => x.Deleted == false);
            if (includes.Length != 0)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            ICollection<T> entities =await query.ToListAsync();
            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
           return await _dbSet.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task SoftDeleteAsync(T entity)
        {
           entity.Deleted = true;
           await UpdateAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
