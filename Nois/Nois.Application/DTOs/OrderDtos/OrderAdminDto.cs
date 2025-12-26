namespace Nois.Application.DTOs.OrderDtos
{
    public class OrderAdminDto
    {
        public int Id { get; init; }
        public string BuyerId { get; init; }
        public string BuyerUserName { get; init; }
        public DateTime OrderDate { get; init; }
        public decimal TotalAmount { get; init; }
        public string Status { get; init; }
    }
}
