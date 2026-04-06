using Nois.Application.DTOs.CategoryDTOs;
using Nois.Application.DTOs.ColorDtos;
using Nois.Domain.Common;

namespace Nois.Application.Interfaces
{
    public interface IColorService
    {
        Task<List<ColorSummaryDto>> GetAllAsync();
        Task<ColorSummaryDto> GetByIdAsync(int id);
		Task<PaginationResult<ColorSummaryDto>> GetPagedAsync(PaginationRequest request);
		Task CreateAsync(CreateColorDto createColorDto);
        Task UpdateAsync(UpdateColorDto updateColorDto);
        Task DeleteAsync(int id);
    }
}
