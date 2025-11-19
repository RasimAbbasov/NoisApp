using Nois.Application.DTOs.CategoryDtos;
using Nois.Application.DTOs.CategoryDTOs;

namespace Nois.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategorySummaryDto>> GetAllAsync();
        Task<CategorySummaryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateCategoryDto createCategoryDto);
        Task UpdateAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteAsync(int id);
    }
}
