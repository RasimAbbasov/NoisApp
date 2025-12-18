using AutoMapper;
using Nois.Application.DTOs.CategoryDTOs;
using Nois.Application.DTOs.ProductVariantDtos;
using Nois.Application.DTOs.WishlistDtos;
using Nois.Application.Interfaces;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;

namespace Nois.Application.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IGenericRepository<Wishlist>  _genericRepository;
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IMapper _mapper;

        public WishlistService(IGenericRepository<Wishlist>  genericRepository, IMapper mapper, IWishlistRepository wishlistRepository)
        {
            _genericRepository = genericRepository;
            _wishlistRepository = wishlistRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateWishlistItemDto createWishlistItemDto)
        {
            var exists = await _genericRepository.ExistsAsync(x => x.UserId == createWishlistItemDto.UserId && x.ProductId == createWishlistItemDto.ProductId);
            if (exists) return;

            var item = _mapper.Map<Wishlist>(createWishlistItemDto);
            item.CreatedAt = DateTime.UtcNow;

            await _genericRepository.CreateAsync(item);
        }

        public async Task RemoveAsync(string userId, int productId)
        {
            await _wishlistRepository.RemoveAsync(userId,productId);
        }

        public async Task<List<WishlistItemDto>> GetUserWishlistAsync(string userId)
        {
            var wishlist = await _wishlistRepository.GetAllWithIncludesAsync(userId);

            return _mapper.Map<List<WishlistItemDto>>(wishlist);
        }

        public async Task<bool> ExistsAsync(string userId, int productId)
        {
            return await _genericRepository.ExistsAsync(x => x.UserId == userId && x.ProductId == productId);
        }
    }

}
