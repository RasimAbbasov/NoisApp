using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.BasketDtos;
using Nois.Application.Interfaces;
using Nois.Application.Services;

namespace Nois.API.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{buyerId}")]
        public async Task<ActionResult<BasketDto>> GetBasket(string buyerId)
        {
            var basketDto = await _basketService.GetBasketAsync(buyerId);
            return Ok(basketDto);
        }

        [HttpPost("{buyerId}/items")]
        public async Task<IActionResult> AddItem(string buyerId, [FromBody] AddToBasketRequest request)
        {
            // Service handles the logic; controller just handles the HTTP response
            await _basketService.AddItemAsync(buyerId, request);

            // Return a standard 200 OK or 204 No Content
            return Ok();
        }

        [HttpDelete("{buyerId}/items/{productId}")]
        public async Task<IActionResult> RemoveItem(string buyerId, int productId)
        {
            await _basketService.RemoveItemFromBasketAsync(buyerId, productId);
            return NoContent(); // Standard HTTP 204 response for successful deletion
        }
    }
}
