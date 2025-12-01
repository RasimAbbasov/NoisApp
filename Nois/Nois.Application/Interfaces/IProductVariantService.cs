using Nois.Application.DTOs.ProductVariantDtos;

namespace Nois.Application.Interfaces
{
    public interface IProductVariantService
    {
        Task<List<ProductVariantSummaryDto>> GetAllAsync();
        Task<ProductVariantSummaryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateProductVariantDto createProductVariantDto);
        Task UpdateAsync(UpdateProductVariantDto updateProductVariantDto);
        Task DeleteAsync(int id);
    }
}
