using Nois.Application.DTOs.ProductVariantDtos;
using Nois.Application.DTOs.PromoCodeDtos;

namespace Nois.Application.Interfaces
{
    public interface IPromoCodeService
    {
		Task<List<PromoCodeDto>> GetAllAsync();
		Task<PromoCodeDto> GetByIdAsync(int id);
		Task CreateAsync(CreatePromoCodeDto createPromoCodeDto);
		Task UpdateAsync(int id, UpdatePromoCodeDto updatePromoCodeDto);
		Task DeleteAsync(int id);
		Task<ApplyPromoCodeResultDto> ApplyPromoCodeAsync(ApplyPromoCodeDto dto);
	}
}
