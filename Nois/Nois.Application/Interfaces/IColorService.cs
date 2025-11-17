using Nois.Application.DTOs.ColorDtos;

namespace Nois.Application.Interfaces
{
    public interface IColorService
    {
        Task<List<ColorDto>> GetAllAsync();
        Task<ColorDto> GetByIdAsync(int id);
        Task CreateAsync(CreateColorDto createColorDto);
        Task UpdateAsync(ColorDto updateColorDto);
        Task DeleteAsync(int id);
    }
}
