using Nois.Application.DTOs.SizeDtos;
using Nois.Domain.Common;

namespace Nois.Application.Interfaces
{
    public interface ISizeService
    {
        Task<List<SizeSummaryDto>> GetAllAsync();
        Task<SizeSummaryDto> GetByIdAsync(int id);
        Task<PaginationResult<SizeSummaryDto>> GetPagedAsync(PaginationRequest request);
		Task CreateAsync(CreateSizeDto createSizeDto);
        Task UpdateAsync(UpdateSizeDto updateSizeDto);
        Task DeleteAsync(int id);
    }
}
