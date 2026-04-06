using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.ProductDtos;
using Nois.Application.DTOs.SizeDtos;
using Nois.Application.Interfaces;
using Nois.Application.Services;
using Nois.Domain.Common;

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
		public async Task<IActionResult> GetPaged([FromQuery] PaginationRequest request)
		{
			var result = await _productService.GetPagedAsync(request);
			_logger.LogInformation("Product GetPaged endpoint called at {Time}", DateTime.Now);
			return Ok(result);
		}

		[HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            _logger.LogInformation("Product GetAll endpoint called at {Time}", DateTime.Now);
            return Ok(products);
        }
        [HttpGet("detailed")]
        public async Task<IActionResult> GetAllDetailed()
        {
            var products = await _productService.GetAllWithDetails();
            _logger.LogInformation("Product GetAllDetailed endpoint called at {Time}", DateTime.Now);
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
        [HttpGet("detailed/{id}")]
        public async Task<IActionResult> GetDetailed(int id)
        {
            var products = await _productService.GetByIdWithDetailsAsync(id);
            _logger.LogInformation("Product GetDetailed endpoint called at {Time}", DateTime.Now);
            return Ok(products);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateProductDto createProductDto)
        {
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

            await _productService.UpdateAsync(dto);
            _logger.LogInformation("Product Update endpoint called at {Time}", DateTime.Now);
            return NoContent();
        }
    }
}
