using Nois.Domain.Entities.Common;
using System.Linq.Expressions;

namespace Nois.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : AuditableEntity
    {
        //IQueryable<T> Table { get; }

        Task<List<T>> GetAllAsync();
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
		Task<T?> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        // Optional additions for flexibility:
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
		Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
		Task<int> SaveChangesAsync(); // In case you're batching operations (recommended)
    }
}
