using Nois.Application.DTOs.CategoryDTOs;
using Nois.Application.DTOs.SizeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
