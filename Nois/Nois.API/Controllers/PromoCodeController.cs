using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.PromoCodeDtos;
using Nois.Application.Interfaces;

namespace Nois.API.Controllers
{
    public class PromoCodeController : BaseController
    {
		private readonly IPromoCodeService _promoCodeService;
		private readonly ILogger<PromoCodeController> _logger;

		public PromoCodeController(IPromoCodeService promoCodeService, ILogger<PromoCodeController> logger)
		{
			_promoCodeService = promoCodeService;
			_logger = logger;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var promoCodes = await _promoCodeService.GetAllAsync();
			_logger.LogInformation("Promo code GetAll endpoint called at {Time}", DateTime.Now);
			return Ok(promoCodes);

		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			if (id <= 0)
				throw new ArgumentException(nameof(id), "Id must be greater than zero.");
			var promoCode = await _promoCodeService.GetByIdAsync(id);
			_logger.LogInformation("Promo code Get endpoint called at {Time}", DateTime.Now);
			if (promoCode == null) return NotFound();
			return Ok(promoCode);
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreatePromoCodeDto createPromoCodeDto)
		{
			await _promoCodeService.CreateAsync(createPromoCodeDto);
			_logger.LogInformation("Promo code Create endpoint called at {Time}", DateTime.Now);
			return Ok(new { message = "Promo code created successfully" });
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			if (id <= 0)
				throw new ArgumentException(nameof(id), "Id must be greater than zero.");

			await _promoCodeService.DeleteAsync(id);
			_logger.LogInformation("Promo code Delete endpoint called at {Time}", DateTime.Now);
			return Ok(new { message = "Promo code deleted successfully" });
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, UpdatePromoCodeDto dto)
		{
			await _promoCodeService.UpdateAsync( id,dto);
			_logger.LogInformation("Promo code {Id} updated at {Time}", id, DateTime.UtcNow);
			return NoContent();
		}

		[HttpPost("apply")]
		public async Task<ActionResult<ApplyPromoCodeResultDto>> ApplyPromoCode([FromBody] ApplyPromoCodeDto dto)
		{
			var result = await _promoCodeService.ApplyPromoCodeAsync(dto);

			if (!result.IsValid)
				return BadRequest(result);

			return Ok(result);
		}
	}
}
