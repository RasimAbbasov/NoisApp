using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.ProductStockDtos;
using Nois.Application.Interfaces;
using Nois.Domain.Common;

namespace Nois.API.Controllers
{
    public class ProductStockController : BaseController
    {
        private readonly IProductStockService _productStockService;
        private readonly ILogger<ProductStockController> _logger;

        public ProductStockController(IProductStockService productStockService, ILogger<ProductStockController> logger)
        {
            _productStockService = productStockService;
            _logger = logger;
        }
		[HttpGet]
		public async Task<IActionResult> GetPaged([FromQuery] PaginationRequest request)
		{
			var result = await _productStockService.GetPagedAsync(request);
			_logger.LogInformation("ProductStock GetPaged endpoint called at {Time}", DateTime.Now);
			return Ok(result);
		}
		[HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var productStocks = await _productStockService.GetAllAsync();
            _logger.LogInformation("ProductStock GetAll endpoint called at {Time}", DateTime.Now);
            return Ok(productStocks);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("Id cannot be less than 0.");

            var productStocks = await _productStockService.GetByIdAsync(id);
            _logger.LogInformation("ProductStock Get endpoint called at {Time}", DateTime.Now);
            return Ok(productStocks);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductStockDto createProductStockDto)
        {
            await _productStockService.CreateAsync(createProductStockDto);
            _logger.LogInformation("ProductStock Create endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "ProductStock created successfully" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id), "Id must be greater than zero.");

            await _productStockService.DeleteAsync(id);
            _logger.LogInformation("ProductStock Delete endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "ProductStock deleted successfully" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductStockDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            await _productStockService.UpdateAsync(dto);
            _logger.LogInformation("ProductStock Update endpoint called at {Time}", DateTime.Now);
            return NoContent();
        }
    }
}
