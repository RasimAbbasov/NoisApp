namespace Nois.Application.DTOs.BasketDtos
{
    public class BasketDto
    {
        public string BuyerId { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public List<BasketItemDto> Items { get; set; } = new();
    }
}
