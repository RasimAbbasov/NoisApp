using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.ProductVariantDtos;
using Nois.Application.Interfaces;

namespace Nois.API.Controllers
{
    public class ProductVariantController : BaseController
    {
        private readonly IProductVariantService _productVariantService;
        private readonly ILogger<ProductVariantController> _logger;

        public ProductVariantController(IProductVariantService productVariantService, ILogger<ProductVariantController> logger)
        {
            _productVariantService = productVariantService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productVariants = await _productVariantService.GetAllAsync();
            _logger.LogInformation("ProductVariant GetAll endpoint called at {Time}", DateTime.Now);
            return Ok(productVariants);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("Id cannot be less than 0.");

            var productVariants = await _productVariantService.GetByIdAsync(id);
            _logger.LogInformation("ProductVariant Get endpoint called at {Time}", DateTime.Now);
            return Ok(productVariants);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVariantDto createProductVariantDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productVariantService.CreateAsync(createProductVariantDto);
            _logger.LogInformation("ProductVariant Create endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "ProductVariant created successfully" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id), "Id must be greater than zero.");

            await _productVariantService.DeleteAsync(id);
            _logger.LogInformation("ProductVariant Delete endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "ProductVariant deleted successfully" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductVariantDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productVariantService.UpdateAsync(dto);
            _logger.LogInformation("ProductVariant Update endpoint called at {Time}", DateTime.Now);
            return NoContent();
        }
    }
}
