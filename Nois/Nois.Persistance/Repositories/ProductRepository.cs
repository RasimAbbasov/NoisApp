using Microsoft.EntityFrameworkCore;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;
using Nois.Persistance.Contexts;

namespace Nois.Persistance.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public readonly NoisDbContext _context;

        public ProductRepository(NoisDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product?>> GetAllWithIncludes()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }

        public async Task<Product?> GetByIdWithIncludes(int id)
        {
            return await _context.Products
                 .Include(x => x.Category)
                 .FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
