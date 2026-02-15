using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.ProductVariantRatingDtos;
using Nois.Application.Interfaces;

namespace Nois.API.Controllers
{
	public class ProductVariantRatingController : BaseController
	{
		private readonly IProductVariantRatingService _ratingService;
		private readonly ILogger<ProductVariantRatingController> _logger;

		public ProductVariantRatingController(
			IProductVariantRatingService ratingService,
			ILogger<ProductVariantRatingController> logger)
		{
			_ratingService = ratingService;
			_logger = logger;
		}

		//Get Ratings By VariantId
		[HttpGet("variant/{variantId}")]
		public async Task<IActionResult> GetByVariantId(int variantId)
		{
			if (variantId <= 0)
				throw new ArgumentOutOfRangeException("VariantId cannot be less than 0.");

			var ratings = await _ratingService.GetByVariantIdAsync(variantId);

			_logger.LogInformation("Ratings fetched for VariantId {VariantId} at {Time}",
				variantId, DateTime.Now);

			return Ok(ratings);
		}

		// Get Rating Summary (Average + Count)
		[HttpGet("variant/{variantId}/summary")]
		public async Task<IActionResult> GetSummary(int variantId)
		{
			if (variantId <= 0)
				throw new ArgumentOutOfRangeException("VariantId cannot be less than 0.");

			var summary = await _ratingService.GetRatingSummaryAsync(variantId);

			_logger.LogInformation("Rating summary fetched for VariantId {VariantId} at {Time}",
				variantId, DateTime.Now);

			return Ok(new
			{
				AverageRating = summary.average,
				RatingCount = summary.count
			});
		}
		// Add or Update Rating
		[HttpPost]
		public async Task<IActionResult> AddRating([FromBody] CreateProductVariantRatingDto dto)
		{
			if (dto == null)
				return BadRequest("Invalid rating data.");

			await _ratingService.AddRatingAsync(dto);

			_logger.LogInformation("Rating added/updated at {Time}", DateTime.Now);

			return Ok("Rating submitted successfully.");
		}

		

		// Delete Rating
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("Id cannot be less than 0.");

			await _ratingService.DeleteAsync(id);

			_logger.LogInformation("Rating deleted at {Time}", DateTime.Now);

			return Ok("Rating deleted successfully.");
		}
	}
}
