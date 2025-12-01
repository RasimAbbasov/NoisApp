using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.ProductDtos;
using Nois.Application.DTOs.SizeDtos;
using Nois.Application.Interfaces;
using Nois.Application.Services;

namespace Nois.API.Controllers
{

    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            _logger.LogInformation("Product Get endpoint called at {Time}", DateTime.Now);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("Id cannot be less than 0.");

            var product = await _productService.GetByIdAsync(id);
            _logger.LogInformation("Product Get endpoint called at {Time}", DateTime.Now);
            return Ok(product);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.CreateAsync(createProductDto);
            _logger.LogInformation("Product Create endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "Product created successfully" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id), "Id must be greater than zero.");

            await _productService.DeleteAsync(id);
            _logger.LogInformation("Product Delete endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "Product deleted successfully" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateProductDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.UpdateAsync(dto);
            _logger.LogInformation("Product Update endpoint called at {Time}", DateTime.Now);
            return NoContent();
        }
    }
}
