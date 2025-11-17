using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.CategoryDTOs;
using Nois.Application.DTOs.ColorDtos;
using Nois.Application.Interfaces;
using Nois.Application.Services;

namespace Nois.API.Controllers
{
    public class ColorController : BaseController
    {
        private IColorService _colorService;
        private readonly ILogger<ColorController> _logger;

        public ColorController(IColorService colorService,ILogger<ColorController> logger)
        {
            _colorService = colorService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var colors = await _colorService.GetAllAsync();
            _logger.LogInformation("Color Get endpoint called at {Time}", DateTime.Now);
            return Ok(colors);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var color = await _colorService.GetByIdAsync(id);
            _logger.LogInformation("Color Get endpoint called at {Time}", DateTime.Now);
            if (color == null) return NotFound();
            return Ok(color);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateColorDto createColorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _colorService.CreateAsync(createColorDto);
            _logger.LogInformation("Color Create endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "Color created successfully" });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _colorService.DeleteAsync(id);
            _logger.LogInformation("Color Delete endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "Color deleted successfully" });
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, ColorDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            await _colorService.UpdateAsync(dto);
            _logger.LogInformation("Color Update endpoint called at {Time}", DateTime.Now);
            return NoContent();
        }
    }
}
