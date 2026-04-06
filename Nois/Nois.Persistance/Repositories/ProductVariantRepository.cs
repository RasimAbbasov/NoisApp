using Microsoft.EntityFrameworkCore;
using Nois.Domain.Common;
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
		public async Task<PaginationResult<ProductVariant>> GetPagedAsync(PaginationRequest request)
		{
			var query = _context.ProductVariants.AsNoTracking();

			var totalRecords = await query.CountAsync();

			var data = await query
                .Include(x => x.Color)
				.Include(x => x.Size)
				.Include(x => x.Product).OrderBy(e => EF.Property<object>(e, "Id"))
				.Skip((request.Page - 1) * request.PageSize)
				.Take(request.PageSize)
				.ToListAsync();

			return new PaginationResult<ProductVariant>(
				data,
				request.Page,
				request.PageSize,
				totalRecords
			);
		}
	}
}
