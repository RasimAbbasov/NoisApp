using Nois.Domain.Entities;

namespace Nois.Domain.Interfaces
{
    public interface IBasketRepository
    {
        Task<Basket?> GetByBuyerIdAsync(string buyerId);
        Task UpsertAsync(Basket basket);
        Task DeleteAsync(int id);
		Task DeleteByBuyerIdAsync(string buyerId);
	}
}
