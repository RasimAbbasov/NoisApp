using Nois.Application.DTOs.ProductStockDtos;
using Nois.Domain.Common;

namespace Nois.Application.Interfaces
{
    public interface IProductStockService
    {
        Task<List<ProductStockSummaryDto>> GetAllAsync();
        Task<ProductStockSummaryDto> GetByIdAsync(int id);
        Task<PaginationResult<ProductStockSummaryDto>> GetPagedAsync(PaginationRequest request);
		Task CreateAsync(CreateProductStockDto createProductStockDto);
        Task UpdateAsync(UpdateProductStockDto updateProductStockDto);
        Task DeleteAsync(int id);
    }
}
