using Nois.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Domain.Interfaces
{
    public interface IPaymentRepository
    {
		Task CreateAsync(Payment payment);
		Task UpdateAsync(Payment payment);
		Task<Payment?> GetByTransactionIdAsync(string transcationId);
	}
}
