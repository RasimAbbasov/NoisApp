using Nois.Application.DTOs.SizeDtos;

namespace Nois.Application.Interfaces
{
    public interface ISizeService
    {
        Task<List<SizeSummaryDto>> GetAllAsync();
        Task<SizeSummaryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateSizeDto createSizeDto);
        Task UpdateAsync(UpdateSizeDto updateSizeDto);
        Task DeleteAsync(int id);
    }
}
