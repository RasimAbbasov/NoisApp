using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.SizeDtos;
using Nois.Application.Interfaces;

namespace Nois.API.Controllers
{
    public class SizeController : BaseController
    {
        private readonly ISizeService _sizeService;
        private readonly ILogger<SizeController> _logger;

        public SizeController(ISizeService SizeService, ILogger<SizeController> logger)
        {
            _sizeService = SizeService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Sizes = await _sizeService.GetAllAsync();
            _logger.LogInformation("Size GetAll endpoint called at {Time}", DateTime.Now);
            return Ok(Sizes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var Size = await _sizeService.GetByIdAsync(id);
            _logger.LogInformation("Size Get endpoint called at {Time}", DateTime.Now);
            if (Size == null) return NotFound();
            return Ok(Size);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSizeDto createSizeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _sizeService.CreateAsync(createSizeDto);
            _logger.LogInformation("Size Create endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "Size created successfully" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sizeService.DeleteAsync(id);
            _logger.LogInformation("Size Delete endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "Size deleted successfully" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSizeDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");
            await _sizeService.UpdateAsync(dto);
            _logger.LogInformation("Size Update endpoint called at {Time}", DateTime.Now);
            return NoContent();
        }
    }
}
