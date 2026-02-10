using Nois.Application.Interfaces;
using Nois.Domain.Entities;
using Nois.Domain.Entities.Enums;
using Nois.Domain.Interfaces;
using Stripe;


namespace Nois.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
		private readonly IOrderRepository _orderRepository;
		private readonly IBasketRepository _basketRepository;

		public PaymentService(IOrderRepository orderRepository,IBasketRepository basketRepository)
		{
			_orderRepository = orderRepository;
			_basketRepository = basketRepository;
		}

		// STEP 1: Create Stripe payment
		public async Task<string> CreatePaymentAsync(Order order)
		{
			var options = new PaymentIntentCreateOptions
			{
				Amount = (long)(order.TotalAmount * 100), // cents
				Currency = "usd",
				Metadata = new Dictionary<string, string>
			{
				{ "orderId", order.Id.ToString() }
			}
			};

			var service = new PaymentIntentService();
			var intent = await service.CreateAsync(options);

			return intent.ClientSecret;
		}

		// STEP 2: Confirm payment via webhook

		public async Task ConfirmPaymentAsync(string paymentIntentId)
		{
			var service = new PaymentIntentService();
			var intent = await service.GetAsync(paymentIntentId);

			if (intent.Status != "succeeded")
				return;

			var orderId = intent.Metadata["orderId"];
			var order = await _orderRepository.GetByIdAsync(Guid.Parse(orderId));

			if (order.Status == OrderStatus.Paid)
				return;

			order.Status = OrderStatus.Paid;

			await _orderRepository.UpdateAsync(order);
			await _basketRepository.DeleteByBuyerIdAsync(order.BuyerId);
		}

	}

}
