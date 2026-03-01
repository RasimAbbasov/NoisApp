using AutoMapper;
using Nois.Application.DTOs.ColorDtos;
using Nois.Application.DTOs.PromoCodeDtos;
using Nois.Application.Exceptions;
using Nois.Application.Interfaces;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;

namespace Nois.Application.Services
{
    public class PromoCodeService : IPromoCodeService
    {
		private readonly IMapper _mapper;
		private readonly IGenericRepository<PromoCode> _genericRepository;
        public PromoCodeService(IMapper mapper, IGenericRepository<PromoCode> genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        public async Task CreateAsync(CreatePromoCodeDto createPromoCodeDto)
        {
			if (createPromoCodeDto == null)
				throw new ArgumentNullException(nameof(createPromoCodeDto));

			var exists = await _genericRepository.ExistsAsync(x => x.Code == createPromoCodeDto.Code);
			if (exists)
				throw new ConflictException("Promo code with this name already exists.");

			var promoCode = _mapper.Map<PromoCode>(createPromoCodeDto);
			promoCode.CreatedAt = DateTime.Now;

			await _genericRepository.CreateAsync(promoCode);
		}

        public async Task DeleteAsync(int id)
        {
			var promoCode = await _genericRepository.GetByIdAsync(id);
			if (promoCode == null) throw new KeyNotFoundException("Promo code not found");

			await _genericRepository.DeleteAsync(promoCode);
		}

        public async Task<List<PromoCodeDto>> GetAllAsync()
        {
			var promoCodes = await _genericRepository.GetAllAsync();
			return _mapper.Map<List<PromoCodeDto>>(promoCodes);
		}

        public async Task<PromoCodeDto> GetByIdAsync(int id)
        {
			var promoCode = await _genericRepository.GetByIdAsync(id);

			if (promoCode == null) throw new KeyNotFoundException($"Promo code with id {id} not found");

			return _mapper.Map<PromoCodeDto>(promoCode);
		}

        public async Task UpdateAsync(int id, UpdatePromoCodeDto updatePromoCodeDto)
        {
			if (updatePromoCodeDto == null) throw new ArgumentNullException(nameof(updatePromoCodeDto));
			var promoCode = await _genericRepository.GetByIdAsync(id);
			if (promoCode == null) throw new KeyNotFoundException("Promo code not found.");

			// "Find if ANY record has this code, WHERE the ID is NOT the one I'm currently editing"
			var exists = await _genericRepository.ExistsAsync(x =>
				x.Code == updatePromoCodeDto.Code && x.Id != id);

			if (exists)
				throw new ConflictException("Promo code with this name already exists on another record.");


			_mapper.Map(updatePromoCodeDto, promoCode);
			promoCode.UpdatedAt = DateTime.UtcNow;

			await _genericRepository.UpdateAsync(promoCode);
		}
		public async Task<ApplyPromoCodeResultDto> ApplyPromoCodeAsync(ApplyPromoCodeDto dto)
		{
			if (string.IsNullOrWhiteSpace(dto.Code))
				return new ApplyPromoCodeResultDto
				{
					IsValid = false,
					Message = "Promo code is required"
				};

			var promo = await _genericRepository
				.GetAsync(x => x.Code == dto.Code);

			if (promo == null)
				return new ApplyPromoCodeResultDto
				{
					IsValid = false,
					Message = "Promo code not found"
				};

			if (!promo.IsActive)
				return new ApplyPromoCodeResultDto
				{
					IsValid = false,
					Message = "Promo code is inactive"
				};

			if (promo.StartDate > DateTime.UtcNow || promo.EndDate < DateTime.UtcNow)
				return new ApplyPromoCodeResultDto
				{
					IsValid = false,
					Message = "Promo code expired or not started"
				};

			if (promo.MaxUsage > 0 && promo.UsedCount >= promo.MaxUsage)
				return new ApplyPromoCodeResultDto
				{
					IsValid = false,
					Message = "Promo code usage limit reached"
				};

			if (dto.OrderAmount < promo.MinOrderAmount)
				return new ApplyPromoCodeResultDto
				{
					IsValid = false,
					Message = $"Minimum order amount is {promo.MinOrderAmount}"
				};

			// ✅ Use AutoMapper here
			var result = _mapper.Map<ApplyPromoCodeResultDto>(promo);

			// Business logic 
			decimal discount = promo.DiscountAmount; // Mebleg olaraq promo code qeyd etmke ucun

			if (promo.DiscountPercent > 0) // Faiz olaraq promo code qeyd etmek ucun
				discount += dto.OrderAmount * (promo.DiscountPercent / 100m);

			result.IsValid = true;
			result.Message = "Promo code applied successfully";
			result.DiscountAmount = discount;

			return result;
		}

	}
}

