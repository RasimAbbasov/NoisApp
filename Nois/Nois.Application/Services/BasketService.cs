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
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<BasketDto> GetBasketAsync(string buyerId)
        {
            var basket = await _basketRepository.GetByBuyerIdAsync(buyerId);

            return _mapper.Map<BasketDto>(basket);
        }

        public async Task AddItemAsync(string buyerId, AddToBasketRequest request)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId)
                ?? throw new Exception("Product Not Found");

            var basket = await _basketRepository.GetByBuyerIdAsync(buyerId)
                ?? new Basket { BuyerId = buyerId };

            var existingItem = basket.Items.FirstOrDefault(x => x.ProductId == request.ProductId);

            if (existingItem != null)
                existingItem.Quantity += request.Quantity;
            else
                basket.Items.Add(new BasketItem
                {
                    ProductId = request.ProductId,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = request.Quantity
                });

            await _basketRepository.UpsertAsync(basket);
        }
        public async Task RemoveItemFromBasketAsync(string buyerId, int productId)
        {
            var basket = await _basketRepository.GetByBuyerIdAsync(buyerId);

            if (basket == null) return;

            var item = basket.Items.FirstOrDefault(x => x.ProductId == productId);

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
