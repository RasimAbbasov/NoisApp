using AutoMapper;
using Nois.Application.DTOs.OrderDtos;
using Nois.Application.Interfaces;
using Nois.Domain.Entities.Enums;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;

namespace Nois.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IBasketRepository basketRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<OrderDto> CreateOrderAsync(string buyerId)
        {
            var basket = await _basketRepository.GetByBuyerIdAsync(buyerId);
            if (basket == null) throw new Exception("Basket Empty");

            var order = new Order
            {
                BuyerId = buyerId,
                OrderItems = basket.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    PriceAtPurchase = i.UnitPrice
                }).ToList(),
                TotalAmount = basket.Items.Sum(i => i.UnitPrice * i.Quantity),
                Status = OrderStatus.Paid
            };

            await _orderRepository.AddAsync(order);
            await _basketRepository.DeleteAsync(basket.Id);

            // Map the saved entity back to DTO for the API response
            return _mapper.Map<OrderDto>(order);
        }
    }
}
