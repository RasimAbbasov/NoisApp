using Microsoft.EntityFrameworkCore;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;
using Nois.Persistance.Contexts;

namespace Nois.Persistance.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public readonly NoisDbContext _context;

        public BasketRepository(NoisDbContext context)
        {
            _context = context;
        }
        public async Task<Basket?> GetByBuyerIdAsync(string buyerId)
        {
            return await _context.Baskets
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.BuyerId == buyerId);
        }
        public async Task UpsertAsync(Basket basket)
        {
            _context.Baskets.Update(basket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var basket = await _context.Baskets.FindAsync(id);
            if (basket != null)
            {
                _context.Baskets.Remove(basket);
                await _context.SaveChangesAsync();
            }
        }
		public async Task DeleteByBuyerIdAsync(string buyerId)
		{
			var basket = await _context.Baskets
				.FirstOrDefaultAsync(x => x.BuyerId == buyerId); // Find yerinə bunu istifadə et

			if (basket != null)
			{
				_context.Baskets.Remove(basket);
				await _context.SaveChangesAsync();
			}
		}

	}
}
