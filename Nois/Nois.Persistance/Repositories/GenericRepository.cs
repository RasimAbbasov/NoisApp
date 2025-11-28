using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Nois.Domain.Entities.Common;
using Nois.Domain.Interfaces;
using Nois.Persistance.Contexts;
using System.Linq.Expressions;

namespace Nois.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : AuditableEntity
    {
        private readonly NoisDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(NoisDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        //public IQueryable<T> Table => _dbSet.AsNoTracking();

        public Task<List<T>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public IQueryable<T> GetAllWithIncludes(params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            foreach (var include in includes)
                query = include(query);

            return query;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return await _dbSet.AnyAsync(predicate);
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
