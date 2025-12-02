using Nois.Domain.Entities;

namespace Nois.Domain.Interfaces
{
    public interface IProductVariantRepository
    {
        Task<List<ProductVariant>> GetAllWithIncludes();
        Task<ProductVariant?> GetByIdWithIncludes(int id);
    }
}
