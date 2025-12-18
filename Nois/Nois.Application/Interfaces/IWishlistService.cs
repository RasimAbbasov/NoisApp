using Nois.Application.DTOs.WishlistDtos;

namespace Nois.Application.Interfaces
{
    public interface IWishlistService
    {
        Task AddAsync(CreateWishlistItemDto createWishlistItemDto);
        Task RemoveAsync(string userId, int productId);
        Task<List<WishlistItemDto>> GetUserWishlistAsync(string userId);
        Task<bool> ExistsAsync(string userId, int productId);
    }

}
