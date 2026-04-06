using Microsoft.EntityFrameworkCore.Storage;
using Nois.Domain.Common;
using Nois.Domain.Entities;

namespace Nois.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order?> GetByIdWithProductStockAsync(Guid id);
        Task<PaginationResult<Order>> GetPagedAsync(PaginationRequest request);
		Task<IEnumerable<Order>> GetByBuyerIdAsync(string buyerId);
        Task<IEnumerable<Order>> GetAllAsync();
        Task AddAsync(Order order);
		Task UpdateAsync(Order order);
        Task<IDbContextTransaction> BeginTransactionAsync();

	}
}
