using Nois.Application.DTOs.ProductDtos;

namespace Nois.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductSummaryDto>> GetAllAsync();
        Task<ProductSummaryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateProductDto createProductDto);
        Task UpdateAsync(UpdateProductDto updateProductDto);
        Task DeleteAsync(int id);
    }
}
