using Microsoft.EntityFrameworkCore;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;
using Nois.Persistance.Contexts;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Nois.Persistance.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly NoisDbContext _context;

        public WishlistRepository(NoisDbContext context)
        {
            _context = context;
        }

        public async Task<List<Wishlist>> GetAllWithIncludesAsync(string userId)
        {
            return await _context.Wishlists
                .Where(x => x.UserId == userId)
                .Include(x => x.Product)
                .ToListAsync();
        }
        public async Task RemoveAsync(string userId, int productId)
        {
            var item = await _context.Wishlists
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

            if (item == null)
                return;

            _context.Wishlists.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
