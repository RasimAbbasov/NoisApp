using Microsoft.EntityFrameworkCore;
using Nois.Domain.Common;
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
		public async Task<PaginationResult<ProductStock>> GetPagedAsync(PaginationRequest request)
		{
			var query = _context.ProductStocks.AsNoTracking();

			var totalRecords = await query.CountAsync();

			var data = await query
				.Include(x => x.ProductVariant)
					.ThenInclude(x => x.Product)
				 .Include(x => x.ProductVariant)
					.ThenInclude(x => x.Color)
				 .Include(x => x.ProductVariant)
					.ThenInclude(x => x.Size)
				.OrderBy(e => EF.Property<object>(e, "Id"))
				.Skip((request.Page - 1) * request.PageSize)
				.Take(request.PageSize)
				.ToListAsync();

			return new PaginationResult<ProductStock>(
				data,
				request.Page,
				request.PageSize,
				totalRecords
			);
		}
	}
}
