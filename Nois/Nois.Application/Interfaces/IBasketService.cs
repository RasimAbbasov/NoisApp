using Nois.Application.DTOs.BasketDtos;

namespace Nois.Application.Interfaces
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string buyerId);
        Task AddItemAsync(string buyerId, AddToBasketRequest request);
        Task RemoveItemFromBasketAsync(string buyerId, int productId);
    }
}
