using Nois.Domain.Entities;

namespace Nois.Domain.Interfaces
{
    public interface IProductStockRepository
    {
        Task<List<ProductStock>> GetAllWithIncludes();
        Task<ProductStock?> GetByIdWithIncludes(int id);
    }
}
