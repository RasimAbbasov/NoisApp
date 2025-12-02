using Nois.Application.DTOs.ProductStockDtos;

namespace Nois.Application.Interfaces
{
    public interface IProductStockService
    {
        Task<List<ProductStockSummaryDto>> GetAllAsync();
        Task<ProductStockSummaryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateProductStockDto createProductStockDto);
        Task UpdateAsync(UpdateProductStockDto updateProductStockDto);
        Task DeleteAsync(int id);
    }
}
