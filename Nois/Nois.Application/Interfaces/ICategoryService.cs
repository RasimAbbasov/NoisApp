using Nois.Application.DTOs.CategoryDtos;
using Nois.Application.DTOs.CategoryDTOs;
using Nois.Domain.Common;

namespace Nois.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategorySummaryDto>> GetAllAsync();
        Task<CategorySummaryDto> GetByIdAsync(int id);
        Task<PaginationResult<CategorySummaryDto>> GetPagedAsync(PaginationRequest request);
		Task CreateAsync(CreateCategoryDto createCategoryDto);
        Task UpdateAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteAsync(int id);
    }
}
