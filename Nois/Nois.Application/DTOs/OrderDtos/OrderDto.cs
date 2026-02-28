namespace Nois.Application.DTOs.OrderDtos
{
    public class OrderDto
    {
        public Guid Id { get; init; }
        public DateTime OrderDate { get; init; }
		public decimal SubTotal { get; init; }      // before discount
		public decimal Discount { get; set; }
		public decimal TotalAmount { get; init; }

		public string Status { get; init; }
        public List<OrderItemDto> Items { get; init; }
		public string ClientSecret { get; set; } = string.Empty;

	}
}
