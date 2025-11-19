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
        Task<List<SizeDto>> GetAllAsync();
        Task<SizeDto> GetByIdAsync(int id);
        Task CreateAsync(CreateSizeDto createSizeDto);
        Task UpdateAsync(SizeDto sizeDto);
        Task DeleteAsync(int id);
    }
}
