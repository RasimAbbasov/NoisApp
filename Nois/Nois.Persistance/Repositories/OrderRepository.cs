using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;
using Nois.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Persistance.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public readonly NoisDbContext _context;
        public OrderRepository(NoisDbContext context)
        {
            _context = context;
        }


        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders
				.AsNoTracking()
				.Include(o => o.OrderItems) // Sifarişin içindəki məhsulları gətir
		          .ThenInclude(oi => oi.ProductVariant) // Hər məhsulun variantını gətir
			        .ThenInclude(pv => pv.ProductStock)
				.Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
		public async Task<Order?> GetByIdWithProductStockAsync(Guid id)
		{
			return await _context.Orders
				.Include(o => o.OrderItems)
					.ThenInclude(oi => oi.ProductVariant)
						.ThenInclude(pv => pv.ProductStock)
				.FirstOrDefaultAsync(o => o.Id == id);
		}

		public async Task<IEnumerable<Order>> GetByBuyerIdAsync(string buyerId)
        {
            return await _context.Orders
                .Where(o => o.BuyerId == buyerId)
                .Include(x=>x.User)
                .Include(o => o.OrderItems)
                .ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(x => x.User)
                .AsNoTracking()
                .ToListAsync();
        }
		public async Task UpdateAsync(Order order)
		{
			_context.Orders.Update(order);
			await _context.SaveChangesAsync();
		}
		public async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return await _context.Database.BeginTransactionAsync();
		}
	}
}
