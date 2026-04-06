using AutoMapper;
using Nois.Application.DTOs.CategoryDtos;
using Nois.Application.DTOs.CategoryDTOs;
using Nois.Application.DTOs.ColorDtos;
using Nois.Application.DTOs.SizeDtos;
using Nois.Application.Exceptions;
using Nois.Application.Interfaces;
using Nois.Domain.Common;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;

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
        public async Task<List<SizeSummaryDto>> GetAllAsync()
        {
            var sizes = await _sizeRepository.GetAllAsync();
            return _mapper.Map<List<SizeSummaryDto>>(sizes);
           
        }
        public async Task<SizeSummaryDto> GetByIdAsync(int id)
        {
            var size = await _sizeRepository.GetByIdAsync(id);
            if (size == null) throw new KeyNotFoundException($"Item with id {id} not found");

            return _mapper.Map<SizeSummaryDto>(size);
        }
		public async Task<PaginationResult<SizeSummaryDto>> GetPagedAsync(PaginationRequest request)
		{
			// Get paginated entities from repository
			var pagedSizes = await _sizeRepository.GetPagedAsync(request);

			// Map entities → DTOs
			var dtoList = _mapper.Map<List<SizeSummaryDto>>(pagedSizes.Items);

			// Return paginated DTO result
			return new PaginationResult<SizeSummaryDto>(
				dtoList,
				pagedSizes.Page,
				pagedSizes.PageSize,
				pagedSizes.TotalRecords
			);
		}
		public async Task CreateAsync(CreateSizeDto createSizeDto)
        {
            if (createSizeDto == null)
                throw new ArgumentNullException(nameof(createSizeDto));

            var exists = await _sizeRepository.ExistsAsync(x => x.Name == createSizeDto.Name);
            if (exists)
                throw new ConflictException("Size with this name already exists.");

            var size = _mapper.Map<Size>(createSizeDto);
            size.CreatedAt = DateTime.Now;
            await _sizeRepository.CreateAsync(size);
        }
        public async Task DeleteAsync(int id)
        {
            var size = await _sizeRepository.GetByIdAsync(id);
            if (size == null) throw new KeyNotFoundException("Size not found.");
            await _sizeRepository.DeleteAsync(size);
        }
        public async Task UpdateAsync(UpdateSizeDto updateSizeDto)
        {
            if (updateSizeDto == null) throw new ArgumentNullException(nameof(updateSizeDto));
            var size = await _sizeRepository.GetByIdAsync(updateSizeDto.Id);
            if (size == null) throw new KeyNotFoundException("Size not found.");

            var exists = await _sizeRepository.ExistsAsync(x => x.Name == updateSizeDto.Name);
            if (exists)
                throw new ConflictException("Size with this name already exists.");

            _mapper.Map(updateSizeDto, size);
            size.UpdatedAt = DateTime.UtcNow;

            await _sizeRepository.UpdateAsync(size);
        }
    }
}
