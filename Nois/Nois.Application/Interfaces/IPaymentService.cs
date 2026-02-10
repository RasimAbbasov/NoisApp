using Nois.Domain.Entities;

namespace Nois.Application.Interfaces
{
    public interface IPaymentService
    {
		Task<string> CreatePaymentAsync(Order order);
		Task ConfirmPaymentAsync(string paymentIntentId);
	}
}
