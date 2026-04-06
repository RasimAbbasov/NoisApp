using Microsoft.EntityFrameworkCore;
using Nois.Domain.Common;
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

        public async Task<List<Product>> GetAllWithIncludes()
        {
            return await _context.Products
                .Include(x => x.Category)
                .Include(x => x.ProductVariants)
                    .ThenInclude(x => x.Color)
                .Include(x => x.ProductVariants)
                    .ThenInclude(x => x.Size)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdWithIncludes(int id)
        {
            return await _context.Products
                 .Include(x => x.Category)
                 .Include(x => x.ProductVariants)
                    .ThenInclude(x => x.Color)
                 .Include(x => x.ProductVariants)
                    .ThenInclude(x => x.Size)
                 .FirstOrDefaultAsync(x=>x.Id == id);
        }
		public async Task<PaginationResult<Product>> GetPagedAsync(PaginationRequest request)
		{
			var query = _context.Products.AsNoTracking();

			var totalRecords = await query.CountAsync();

			var data = await query
                .Include(x => x.Category)
				.OrderBy(e => EF.Property<object>(e, "Id"))
				.Skip((request.Page - 1) * request.PageSize)
				.Take(request.PageSize)
				.ToListAsync();

			return new PaginationResult<Product>(
				data,
				request.Page,
				request.PageSize,
				totalRecords
			);
		}
	}
}
