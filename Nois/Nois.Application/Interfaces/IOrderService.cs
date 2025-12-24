using Nois.Application.DTOs.OrderDtos;

namespace Nois.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(string buyerId);
    }
}
