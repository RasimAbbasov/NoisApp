using Nois.Application.DTOs.OrderDtos;

namespace Nois.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderAdminDto>> GetAllOrdersAsync();
		Task<List<OrderAdminDto>> GetOrderByUserAsync(string UserId);

		Task<OrderDto> CreateOrderAsync(string buyerId);
    }
}
