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
		private readonly IPaymentRepository _paymentRepository;
		private readonly IGenericRepository<PromoCode> _promoCodeRepository;

		public PaymentService(IOrderRepository orderRepository,IBasketRepository basketRepository,IPaymentRepository paymentRepository,IProductStockRepository productStockRepository, IGenericRepository<PromoCode> promoCodeRepository)
		{
			_orderRepository = orderRepository;
			_basketRepository = basketRepository;
			_paymentRepository = paymentRepository;
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
				PaymentMethod = "pm_card_visa", //temporary for testing
				Confirm = true, //temporary for testing
				// BU HİSSƏNİ ƏLAVƏ ET:
				AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
				{
					Enabled = true, // Stripe Dashboard-da aktiv etdiyin bütün metodları frontend-ə göndərir
					AllowRedirects = "never"  //temporary for testing
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

				var payment = new Payment
				{
					OrderId = order.Id,
					TransactionId = intent.Id, // Stripe-dan gələn ID
					Amount = order.TotalAmount,
					PaidAt = DateTime.UtcNow
					
				};

				await _paymentRepository.CreateAsync(payment);
				// Frontend-ə həm ClientSecret, həm də lazımdırsa digər datanı qaytarırıq
				return intent.ClientSecret;
			}
			catch (StripeException e)
			{
				Console.WriteLine(e.StripeError.Message);
				return e.StripeError.Message;
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
			using var transaction = await _orderRepository.BeginTransactionAsync();

			try
			{
				if (order.Payment != null)
				{
					order.Payment.PaidAt = DateTime.UtcNow; // İndi həqiqətən ödənildi
					order.Payment.IsSuccess = true;
				}

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
				await transaction.CommitAsync();
			}
			catch (Exception)
			{
				// Hər hansı xəta olarsa (məsələn, səbət silinməsə), 
				// edilən bütün DB dəyişikliklərini (stok, status və s.) geri qaytar
				await transaction.RollbackAsync();
				throw;
			}

		}
	}

}
