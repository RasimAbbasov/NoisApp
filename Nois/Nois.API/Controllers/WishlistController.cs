using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.WishlistDtos;
using Nois.Application.Interfaces;

namespace Nois.API.Controllers
{
    public class WishlistController : BaseController
    {
        private readonly IWishlistService  _wishlistService;
        private readonly ILogger<WishlistController> _logger;
        public WishlistController(IWishlistService wishlistService, ILogger<WishlistController> logger)
        {
            _wishlistService = wishlistService;
            _logger = logger;
        }
    
        [HttpGet("{userid}")]
        public async Task<IActionResult> GetUserWishlist(string userid)
        {
            var list = await _wishlistService.GetUserWishlistAsync(userid);
            _logger.LogInformation("Wishlist GetUserWishlist endpoint called at {Time}", DateTime.Now);
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserWishlistItem(CreateWishlistItemDto createWishlistItemDto)
        {
            await _wishlistService.AddAsync(createWishlistItemDto);
            _logger.LogInformation("Wishlist AddUserWishlistItem endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "Item added successfully" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemFromWishlist(string userId,int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id), "Id must be greater than zero.");

            await _wishlistService.RemoveAsync(userId, id);
            _logger.LogInformation("Wishlist DeleteItemFromWishlist endpoint called at {Time}", DateTime.Now);
            return Ok(new { message = "Item deleted successfully from list"});
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id,  dto)
        //{
        //    if (id != dto.Id)
        //        return BadRequest("ID mismatch.");

        //    await _categoryService.UpdateAsync(dto);
        //    _logger.LogInformation("Category Update endpoint called at {Time}", DateTime.Now);
        //    return NoContent();
        //}
    }
}
