using AutoMapper;
using Nois.Application.DTOs.ColorDtos;
using Nois.Application.Exceptions;
using Nois.Application.Interfaces;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;

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
            if (createColorDto == null)
                throw new ArgumentNullException(nameof(createColorDto));

            var exists = await _colorRepository.ExistsAsync(x => x.Name == createColorDto.Name);
            if (exists)
                throw new ConflictException("Color with this name already exists.");

            var color = _mapper.Map<Color>(createColorDto);
            color.CreatedAt = DateTime.Now;

            await _colorRepository.CreateAsync(color);
        }

        public async Task DeleteAsync(int id)
        {
            var color = await _colorRepository.GetByIdAsync(id);
            if (color == null) throw new KeyNotFoundException("Color not found");

            await _colorRepository.DeleteAsync(color);
        }

        public async Task<List<ColorSummaryDto>> GetAllAsync()
        {
           var colors = await _colorRepository.GetAllAsync();
           return _mapper.Map<List<ColorSummaryDto>>(colors);
        }

        public async Task<ColorSummaryDto> GetByIdAsync(int id)
        {
            var entity = await _colorRepository.GetByIdAsync(id);

            if (entity == null) throw new KeyNotFoundException($"Item with id {id} not found");

            return _mapper.Map<ColorSummaryDto>(entity);
        }



        public async Task UpdateAsync(UpdateColorDto updateColorDto)
        {
            if (updateColorDto == null) throw new ArgumentNullException(nameof(updateColorDto));
            var color = await _colorRepository.GetByIdAsync(updateColorDto.Id);
            if (color == null) throw new KeyNotFoundException("Color not found.");

            var exists = await _colorRepository.ExistsAsync(x => x.Name == updateColorDto.Name);
            if (exists)
                throw new ConflictException("Color with this name already exists.");


            _mapper.Map(updateColorDto, color);
            color.UpdatedAt = DateTime.UtcNow;

            await _colorRepository.UpdateAsync(color);
        }
    }
}
