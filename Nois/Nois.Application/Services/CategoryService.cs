using AutoMapper;
using Nois.Application.DTOs.CategoryDtos;
using Nois.Application.DTOs.CategoryDTOs;
using Nois.Application.Exceptions;
using Nois.Application.Interfaces;
using Nois.Domain.Entities;
using Nois.Persistance.Repositories.Interfaces;

namespace Nois.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null)
                throw new ArgumentNullException(nameof(createCategoryDto));

            var exists = await _categoryRepository.ExistsAsync(x => x.Name == createCategoryDto.Name);
            if (exists)
                throw new ConflictException("Category with this name already exists.");


            var category = _mapper.Map<Category>(createCategoryDto);
            category.CreatedAt = DateTime.UtcNow;

            await _categoryRepository.CreateAsync(category);
        }


        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id), "Id must be greater than zero.");

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException("Category not found.");

            await _categoryRepository.DeleteAsync(category);
        }
            
        public async Task<List<CategorySummaryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync(); 

            var dtoList = _mapper.Map<List<CategorySummaryDto>>(categories);
            return dtoList;

        }

        public async Task<CategorySummaryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if(category == null) throw new KeyNotFoundException($"Item with id {id} not found");

            return _mapper.Map<CategorySummaryDto>(category);
        }

        public async Task UpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            if(updateCategoryDto == null) throw new ArgumentNullException(nameof(updateCategoryDto));
            var category = await _categoryRepository.GetByIdAsync(updateCategoryDto.Id);
            if( category == null) throw new KeyNotFoundException("Category not found.");

            var exists = await _categoryRepository.ExistsAsync(x => x.Name == updateCategoryDto.Name);
            if (exists)
                throw new ConflictException("Category with this name already exists.");


            _mapper.Map(updateCategoryDto, category);
            category.UpdatedAt = DateTime.UtcNow;

            await _categoryRepository.UpdateAsync(category);
        }
    }
}
