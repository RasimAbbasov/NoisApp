using AutoMapper;
using Nois.Application.DTOs.CategoryDTOs;
using Nois.Application.DTOs.ColorDtos;
using Nois.Application.Interfaces;
using Nois.Domain.Entities;
using Nois.Persistance.Repositories.Interfaces;

namespace Nois.Application.Services
{
    public class ColorService : IColorService
    {
        private readonly IGenericRepository<Color> _colorRepository;
        private readonly IMapper _mapper;
        public ColorService(IMapper mapper,IGenericRepository<Color> colorRepository) 
        {
         _mapper = mapper;
         _colorRepository = colorRepository;
        }
        public async Task CreateAsync(CreateColorDto createColorDto)
        {
            var color = _mapper.Map<Color>(createColorDto);
            color.CreatedAt = DateTime.Now;

            await _colorRepository.CreateAsync(color);
        }

        public async Task DeleteAsync(int id)
        {
            var color = await _colorRepository.GetByIdAsync(id);
            if (color == null) throw new KeyNotFoundException();

            await _colorRepository.DeleteAsync(color);
        }

        public async Task<List<ColorDto>> GetAllAsync()
        {
           var colors = await _colorRepository.GetAllAsync();
           var list = _mapper.Map<List<ColorDto>>(colors);
           return list;
        }

        public async Task<ColorDto> GetByIdAsync(int id)
        {
            var colorEntity = await _colorRepository.GetByIdAsync(id);
            var dto = _mapper.Map<ColorDto>(colorEntity);
            return dto;
        }


        public async Task UpdateAsync(ColorDto updateColorDto)
        {
            var color = await _colorRepository.GetByIdAsync(updateColorDto.Id);
            if (color == null) throw new KeyNotFoundException();


            _mapper.Map(updateColorDto, color);
            color.UpdatedAt = DateTime.UtcNow;

            await _colorRepository.UpdateAsync(color);
        }
    }
}
