namespace Nois.Application.DTOs.OrderDtos
{
    public record OrderItemDto(int ProductId, int Quantity, decimal PriceAtPurchase);
}
