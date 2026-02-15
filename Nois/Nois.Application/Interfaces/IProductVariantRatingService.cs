using Nois.Application.DTOs.ProductDtos;
using Nois.Application.DTOs.ProductVariantRatingDtos;

namespace Nois.Application.Interfaces
{
    public interface IProductVariantRatingService
    {
		Task<List<ProductVariantRatingDto>> GetByVariantIdAsync(int id);
		Task AddRatingAsync(CreateProductVariantRatingDto dto);
		Task<(double average, int count)> GetRatingSummaryAsync(int variantId);
		Task DeleteAsync(int id);
	}
}
