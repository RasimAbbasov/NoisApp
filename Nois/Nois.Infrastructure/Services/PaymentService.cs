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
		private readonly IProductStockRepository _productStockRepository;
		private readonly IGenericRepository<PromoCode> _promoCodeRepository;

		public PaymentService(IOrderRepository orderRepository,IBasketRepository basketRepository,IProductStockRepository productStockRepository, IGenericRepository<PromoCode> promoCodeRepository)
		{
			_orderRepository = orderRepository;
			_basketRepository = basketRepository;
			_promoCodeRepository = promoCodeRepository;
			_productStockRepository = productStockRepository;
		}

		// STEP 1: Create Stripe payment
		public async Task<string> CreatePaymentAsync(Order order)
		{
			var options = new PaymentIntentCreateOptions
			{
				Amount = (long)(order.TotalAmount * 100), // cents
				Currency = "usd",
				// BU HİSSƏNİ ƏLAVƏ ET:
				AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
				{
					Enabled = true, // Stripe Dashboard-da aktiv etdiyin bütün metodları frontend-ə göndərir
				},
				Metadata = new Dictionary<string, string>
		{
			{ "orderId", order.Id.ToString() }

		}
			};

			try
			{
				var service = new PaymentIntentService();
				var intent = await service.CreateAsync(options);

				// Frontend-ə həm ClientSecret, həm də lazımdırsa digər datanı qaytarırıq
				return intent.ClientSecret;
			}
			catch (StripeException e)
			{
				Console.WriteLine(e.StripeError.Message);
				return null;
			}
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

			foreach (var item in order.OrderItems) //ProductStock-un update olub, azaltmaq ucun
			{
				var stock = item.ProductVariant.ProductStock;

				if (stock.Quantity < item.Quantity)
					throw new Exception("Insufficient stock");

				stock.Quantity -= item.Quantity;
			}

			if (order.PromoCodeId.HasValue) 
			{
				var promo = await _promoCodeRepository.GetByIdAsync(order.PromoCodeId.Value);
				promo.UsedCount++;
			}

			await _orderRepository.UpdateAsync(order);
			await _basketRepository.DeleteByBuyerIdAsync(order.BuyerId);
		}

	}

}
