using Microsoft.EntityFrameworkCore;
using Nois.Application.Interfaces;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;
using Nois.Persistance.Contexts;

namespace Nois.Persistance.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
		public readonly NoisDbContext _context;
		public PaymentRepository(NoisDbContext context)
		{
			_context = context;
		}
		public async Task CreateAsync(Payment payment)
		{
			if (payment == null) throw new ArgumentNullException(nameof(payment));

			await _context.AddAsync(payment);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(Payment payment)
		{
			if (payment == null) throw new ArgumentNullException(nameof(payment));

			_context.Entry(payment).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
		public async Task<Payment?> GetByTransactionIdAsync(string transcationId)
		{
			return await _context.Payments.FirstOrDefaultAsync(x => x.TransactionId == transcationId);
		}
	}
}
