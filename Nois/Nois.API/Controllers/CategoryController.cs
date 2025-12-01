using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.CategoryDtos;
using Nois.Application.DTOs.CategoryDTOs;
using Nois.Application.Interfaces;


namespace Nois.API.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryService categoryService,ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            _logger.LogInformation("Category Get endpoint called at {Time}", DateTime.Now);
            return Ok(categories);

        }
        [HttpGet("{id}")]
         public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id), "Id must be greater than zero.");

            var category = await _categoryService.GetByIdAsync(id);
            _logger.LogInformation("Category Get endpoint called at {Time}", DateTime.Now);
            if (category == null) return NotFound();
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _categoryService.CreateAsync(createCategoryDto);
            _logger.LogInformation("Category Create endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "Category created successfully" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id), "Id must be greater than zero.");

            await _categoryService.DeleteAsync(id);
            _logger.LogInformation("Category Delete endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "Category deleted successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            await _categoryService.UpdateAsync(dto);
            _logger.LogInformation("Category Update endpoint called at {Time}", DateTime.Now);
            return NoContent();
        }
    }
}
