using Nois.Domain.Entities;

namespace Nois.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetByBuyerIdAsync(string buyerId);
        Task AddAsync(Order order);
    }
}
