using Nois.Domain.Common;
using Nois.Domain.Entities;

namespace Nois.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllWithIncludes();
        Task<Product?> GetByIdWithIncludes(int id);
		Task<PaginationResult<Product>> GetPagedAsync(PaginationRequest request);

	}
}
