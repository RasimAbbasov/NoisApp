using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nois.Application.DTOs.ColorDtos;
using Nois.Application.DTOs.ProductDtos;
using Nois.Application.DTOs.ProductVariantRatingDtos;
using Nois.Application.Interfaces;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Nois.Application.Services
{
    public class ProductVariantRatingService : IProductVariantRatingService
    {
        private readonly IGenericRepository<ProductVariantRating> _genericRepository;
		private readonly IMapper _mapper;
        public ProductVariantRatingService(IGenericRepository<ProductVariantRating> genericRepository,IMapper mapper) 
        {
         _genericRepository = genericRepository;
	     _mapper = mapper;
        }
		public async Task AddRatingAsync(CreateProductVariantRatingDto dto)
		{
			if (dto == null)
				throw new ArgumentNullException(nameof(dto));

			if (dto.Stars < 1 || dto.Stars > 5)
				throw new ArgumentException("Rating must be between 1 and 5.");

			// Get existing rating (not just check existence)
			var existingRating = await _genericRepository
				.GetAsync(x => x.ProductVariantId == dto.ProductVariantId
							&& x.UserId == dto.UserId);

			if (existingRating != null)
			{
				// Update rating
				existingRating.Stars = dto.Stars;
				existingRating.UpdatedAt = DateTime.UtcNow;
				existingRating.Comment = dto.Comment;
				await _genericRepository.UpdateAsync(existingRating);
			}
			else
			{
				var rating = _mapper.Map<ProductVariantRating>(dto);
				await _genericRepository.CreateAsync(rating);
			}
		}


		public async Task DeleteAsync(int id)
        {
			var rating = await _genericRepository.GetByIdAsync(id);
			if (rating == null) throw new KeyNotFoundException("Rating not found");

			await _genericRepository.DeleteAsync(rating);

        }

		public async Task<List<ProductVariantRatingDto>> GetByVariantIdAsync(int variantId)
		{
			var ratings = await _genericRepository
				.GetAllAsync(x => x.ProductVariantId == variantId);

			return _mapper.Map<List<ProductVariantRatingDto>>(ratings);
		}
		public async Task<(double average, int count)> GetRatingSummaryAsync(int variantId)
		{
			var ratings = await _genericRepository
				.GetAllAsync(x => x.ProductVariantId == variantId);

			var count = ratings.Count;
			var average = count == 0 ? 0 : ratings.Average(x => x.Stars);

			return (average, count);
		}

	}
}
