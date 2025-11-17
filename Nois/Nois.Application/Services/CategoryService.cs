using AutoMapper;
using Nois.Application.DTOs.CategoryDTOs;
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
            var category = _mapper.Map<Category>(createCategoryDto);
            category.CreatedAt = DateTime.Now;

            await _categoryRepository.CreateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException();

            await _categoryRepository.DeleteAsync(category);
        }
            
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync(); // Entity Framework Core gore error verir, istifade olunsa Onion arcihecture pozulacaq.

            var dtoList = _mapper.Map<List<CategoryDto>>(categories);
            return dtoList;

        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var categoryDto = await _categoryRepository.GetByIdAsync(id);
            if(categoryDto == null) throw new KeyNotFoundException();

            var category = _mapper.Map<CategoryDto>(categoryDto);

            return category;
        }

        public async Task UpdateAsync(CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDto.Id);
            if( category == null) throw new KeyNotFoundException();


            _mapper.Map(categoryDto, category);
            category.UpdatedAt = DateTime.UtcNow;

            await _categoryRepository.UpdateAsync(category);
        }
    }
}
