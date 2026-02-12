using AutoMapper;
using Nois.Application.DTOs.OrderDtos;
using Nois.Application.Interfaces;
using Nois.Domain.Entities.Enums;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;
using Nois.Application.DTOs.ColorDtos;

namespace Nois.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public OrderService(IBasketRepository basketRepository, IPaymentService paymentService ,IOrderRepository orderRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _orderRepository = orderRepository;
            _paymentService = paymentService;
			_mapper = mapper;
        }

        public async Task<List<OrderAdminDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<OrderAdminDto>>(orders);
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
                    ProductVariantId = i.ProductVariantId,
                    Quantity = i.Quantity,
                    PriceAtPurchase = i.UnitPrice
                }).ToList(),
                TotalAmount = basket.Items.Sum(i => i.UnitPrice * i.Quantity),
				Status = OrderStatus.Pending
			};

			await _orderRepository.AddAsync(order);
			var clientSecret = await _paymentService.CreatePaymentAsync(order);
			var orderDto = _mapper.Map<OrderDto>(order);
			orderDto.ClientSecret = clientSecret;
			return orderDto;
		}
    }
}
