using Nois.Domain.Entities;

namespace Nois.Domain.Interfaces
{
    public interface IWishlistRepository
    {
        Task<List<Wishlist>> GetAllWithIncludesAsync(string userId);
        Task RemoveAsync(string userId, int productId);
    }
}
