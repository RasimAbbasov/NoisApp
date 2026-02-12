using AutoMapper;
using Nois.Application.DTOs.BasketDtos;
using Nois.Application.Interfaces;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;

namespace Nois.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IProductVariantRepository productVariantRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _productVariantRepository = productVariantRepository;
            _mapper = mapper;
        }

        public async Task<BasketDto> GetBasketAsync(string buyerId)
        {
            var basket = await _basketRepository.GetByBuyerIdAsync(buyerId);

            return _mapper.Map<BasketDto>(basket);
        }

        public async Task AddItemAsync(string buyerId, AddToBasketRequest request)
        {
			var productVariant = await _productVariantRepository.GetByIdWithIncludes(request.ProductVariantId)
				?? throw new Exception("Product Not Found");

            var basket = await _basketRepository.GetByBuyerIdAsync(buyerId)
                ?? new Basket { BuyerId = buyerId };

            var existingItem = basket.Items.FirstOrDefault(x => x.ProductVariantId == request.ProductVariantId);

            if (existingItem != null)
                existingItem.Quantity += request.Quantity;
            else
                basket.Items.Add(new BasketItem
                {
					ProductVariantId = request.ProductVariantId,
					ProductName = productVariant.Product.Name,
					UnitPrice = productVariant.Product.Price,
					Quantity = request.Quantity
                });

            await _basketRepository.UpsertAsync(basket);
        }
        //public async Task UpdateItemQuantityAsync(string buyerId, int productId, int newQuantity)
        //{
        //    // 1. Minimum quantity safety
        //    if (newQuantity <= 0)
        //    {
        //        await RemoveItemFromBasketAsync(buyerId, productId);
        //        return;
        //    }

        //    var basket = await _basketRepository.GetByBuyerIdAsync(buyerId);
        //    var item = basket?.Items.FirstOrDefault(x => x.ProductId == productId);

        //    if (item != null)
        //    {
        //        // 2. Stock Check (Optional but recommended)
        //        var product = await _productRepository.GetByIdAsync(productId);
        //        if (product != null && product. < newQuantity)
        //            throw new Exception("Insufficient stock");

        //        // 3. Update the value
        //        item.Quantity = newQuantity;
        //        await _basketRepository.UpsertAsync(basket);
        //    }
        //}
        public async Task RemoveItemFromBasketAsync(string buyerId, int productVariantId)
        {
            var basket = await _basketRepository.GetByBuyerIdAsync(buyerId);

            if (basket == null) return;

            var item = basket.Items.FirstOrDefault(x => x.ProductVariantId == productVariantId);

            if (item == null) return;

            // 3. Logic: If quantity > 1, reduce it. If it's the last one, remove it.
            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                basket.Items.Remove(item);
            }

            basket.UpdatedAt = DateTime.UtcNow;

            await _basketRepository.UpsertAsync(basket);
        }
    }

}
