using AutoMapper;
using Nois.Application.DTOs.OrderDtos;
using Nois.Application.DTOs.ProductDtos;
using Nois.Application.DTOs.PromoCodeDtos;
using Nois.Application.Interfaces;
using Nois.Domain.Common;
using Nois.Domain.Entities;
using Nois.Domain.Entities.Enums;
using Nois.Domain.Interfaces;

namespace Nois.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
		private readonly IPromoCodeService _promoCodeService;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public OrderService(IBasketRepository basketRepository,IPromoCodeService promoCodeService, IPaymentService paymentService ,IOrderRepository orderRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
			_promoCodeService = promoCodeService;
            _orderRepository = orderRepository;
            _paymentService = paymentService;
			_mapper = mapper;
        }

        public async Task<List<OrderAdminDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<OrderAdminDto>>(orders);
        }
		public async Task<List<OrderAdminDto>> GetOrderByUserAsync(string UserId)
		{
			var orders = await _orderRepository.GetByBuyerIdAsync(UserId);
			return _mapper.Map<List<OrderAdminDto>>(orders);
		}
		public async Task<PaginationResult<OrderAdminDto>> GetPagedAsync(PaginationRequest request)
		{
			// Get paginated entities from repository
			var pagedOrders = await _orderRepository.GetPagedAsync(request);

			// Map entities → DTOs
			var dtoList = _mapper.Map<List<OrderAdminDto>>(pagedOrders.Items);

			// Return paginated DTO result
			return new PaginationResult<OrderAdminDto>(
				dtoList,
				pagedOrders.Page,
				pagedOrders.PageSize,
				pagedOrders.TotalRecords
			);
		}
		public async Task<OrderDto> CreateOrderAsync(CreateOrderRequestDto request)
        {
            var basket = await _basketRepository.GetByBuyerIdAsync(request.BuyerId);
            if (basket == null) throw new Exception("Basket Empty");

			var totalAmount = basket.Items.Sum(i => i.UnitPrice * i.Quantity);

			var order = new Order
            {
                BuyerId = request.BuyerId,
                OrderItems = basket.Items.Select(i => new OrderItem
                {
                    ProductVariantId = i.ProductVariantId,
                    Quantity = i.Quantity,
                    PriceAtPurchase = i.UnitPrice
                }).ToList(),
                TotalAmount = totalAmount,
				Status = OrderStatus.Pending
			};

			decimal discount = 0;

			if (!string.IsNullOrEmpty(request.PromoCode))
			{
				var promoResult = await _promoCodeService.ApplyPromoCodeAsync(
					new ApplyPromoCodeDto
					{
						Code = request.PromoCode,
						OrderAmount = totalAmount,
						BuyerId = request.BuyerId
					});

				if (!promoResult.IsValid)
					throw new Exception(promoResult.Message);

				discount = promoResult.DiscountAmount;
				order.TotalAmount -= discount;
				order.PromoCodeId = promoResult.PromoCodeId;
			}
			await _orderRepository.AddAsync(order);

			var clientSecret = await _paymentService.CreatePaymentAsync(order);

			var orderDto = _mapper.Map<OrderDto>(order);

			orderDto.ClientSecret = clientSecret;
			orderDto.Discount = discount;

			return orderDto;
		}
    }
}
