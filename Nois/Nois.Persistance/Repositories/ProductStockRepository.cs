using Microsoft.EntityFrameworkCore;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;
using Nois.Persistance.Contexts;

namespace Nois.Persistance.Repositories
{
    public class ProductStockRepository : IProductStockRepository
    {
        public readonly NoisDbContext _context;

        public ProductStockRepository(NoisDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductStock>> GetAllWithIncludes()
        {
            return await _context.ProductStocks
                 .Include(x => x.ProductVariant)
                    .ThenInclude(x=>x.Product)
                 .Include(x=>x.ProductVariant)
                    .ThenInclude(x=>x.Color)
                 .Include(x=>x.ProductVariant)
                    .ThenInclude(x=>x.Size)
                 .ToListAsync();
        }

        public async Task<ProductStock?> GetByIdWithIncludes(int id)
        {
            return await _context.ProductStocks
                 .Include(x => x.ProductVariant)
                    .ThenInclude(x => x.Product)
                 .Include(x => x.ProductVariant)
                    .ThenInclude(x => x.Color)
                 .Include(x => x.ProductVariant)
                    .ThenInclude(x => x.Size)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
