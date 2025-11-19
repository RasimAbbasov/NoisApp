using Nois.Application.DTOs.ColorDtos;

namespace Nois.Application.Interfaces
{
    public interface IColorService
    {
        Task<List<ColorSummaryDto>> GetAllAsync();
        Task<ColorSummaryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateColorDto createColorDto);
        Task UpdateAsync(UpdateColorDto updateColorDto);
        Task DeleteAsync(int id);
    }
}
