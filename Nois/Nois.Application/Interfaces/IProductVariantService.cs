using Nois.Application.DTOs.ProductVariantDtos;
using Nois.Domain.Common;

namespace Nois.Application.Interfaces
{
    public interface IProductVariantService
    {
        Task<List<ProductVariantSummaryDto>> GetAllAsync();
        Task<ProductVariantSummaryDto> GetByIdAsync(int id);
		Task<PaginationResult<ProductVariantSummaryDto>> GetPagedAsync(PaginationRequest request);
		Task CreateAsync(CreateProductVariantDto createProductVariantDto);
        Task UpdateAsync(UpdateProductVariantDto updateProductVariantDto);
        Task DeleteAsync(int id);
    }
}
