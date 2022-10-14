using Domain.Commons;
using Microsoft.EntityFrameworkCore;
using PYP_Book.Application.Common.Interfaces.Repositories.GenericRepository;
using PYP_Book.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Infrastructure.Common.Repositories.GenericRepository
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
            return entity;
        }


        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool getDeleted = false, params string[] includes)
        {
            IQueryable<T> query = expression is null ? _dbSet.AsQueryable() : _dbSet.Where(expression);
            query = query.Where(x => x.Deleted == getDeleted);
            if (includes.Length != 0)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            ICollection<T> entities = await query.ToListAsync();
            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.Deleted == false);
        }
        public async Task<T> GetByIdWithIncludesAsync(int id, params string[] includes)
        {
            IQueryable<T> query = _dbSet.Where(x => x.Id == id && x.Deleted == false).AsQueryable();
            if (includes.Length != 0)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return await query.SingleOrDefaultAsync();
        }

        public void SoftDelete(T entity)
        {
            entity.Deleted = true;
            Update(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
