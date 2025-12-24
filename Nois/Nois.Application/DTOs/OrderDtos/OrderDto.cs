namespace Nois.Application.DTOs.OrderDtos
{
    public class OrderDto
    {
        public int Id { get; init; }
        public DateTime OrderDate { get; init; }
        public decimal TotalAmount { get; init; }
        public string Status { get; init; }
        public List<OrderItemDto> Items { get; init; }
    }
}
