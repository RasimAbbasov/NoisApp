using Microsoft.EntityFrameworkCore;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;
using Nois.Persistance.Contexts;

namespace Nois.Persistance.Repositories
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        public readonly NoisDbContext _context;

        public ProductVariantRepository(NoisDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductVariant>> GetAllWithIncludes()
        {
           return await _context.ProductVariants
                .Include(x => x.Color)
                .Include(x=>x.Size)
                .Include(x=>x.Product)
                .ToListAsync();
        }

        public async Task<ProductVariant?> GetByIdWithIncludes(int id)
        {
            return await _context.ProductVariants
                 .Include(x => x.Product)
                 .Include(x => x.Size)
                 .Include(x => x.Color)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
