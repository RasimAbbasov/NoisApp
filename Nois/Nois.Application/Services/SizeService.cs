using AutoMapper;
using Nois.Application.DTOs.SizeDtos;
using Nois.Application.Interfaces;
using Nois.Domain.Entities;
using Nois.Persistance.Repositories.Interfaces;

namespace Nois.Application.Services
{
    public class SizeService : ISizeService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Size> _sizeRepository;
        public SizeService(IMapper mapper, IGenericRepository<Size> sizeRepository)
        {
            _mapper = mapper;
            _sizeRepository = sizeRepository;
        }
        public async Task<List<SizeDto>> GetAllAsync()
        {
            var sizes = await _sizeRepository.GetAllAsync();
            var list = _mapper.Map<List<SizeDto>>(sizes);
            return list;
        }
        public async Task<SizeDto> GetByIdAsync(int id)
        {
            var size = await _sizeRepository.GetByIdAsync(id);
            var dto = _mapper.Map<SizeDto>(size);
            return dto;
        }
        public async Task CreateAsync(CreateSizeDto createSizeDto)
        {
            var size = _mapper.Map<Size>(createSizeDto);
            size.CreatedAt = DateTime.Now;
            await _sizeRepository.CreateAsync(size);
        }
        public async Task DeleteAsync(int id)
        {
            var size = await _sizeRepository.GetByIdAsync(id);
            if (size == null) throw new KeyNotFoundException();
            await _sizeRepository.DeleteAsync(size);
        }
        public async Task UpdateAsync(SizeDto sizeDto)
        {
            var size = await _sizeRepository.GetByIdAsync(sizeDto.Id);
            if (size == null) throw new KeyNotFoundException();


            _mapper.Map(sizeDto, size);
            size.UpdatedAt = DateTime.UtcNow;

            await _sizeRepository.UpdateAsync(size);
        }
    }
}
