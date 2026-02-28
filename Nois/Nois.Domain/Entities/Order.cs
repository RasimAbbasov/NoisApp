using Nois.Domain.Entities.Common;
using Nois.Domain.Entities.Enums;
using Nois.Domain.Entities.Identity;

namespace Nois.Domain.Entities
{
    public class Order 
    {
        public Guid Id { get; set; }
        public string BuyerId { get; set; } = string.Empty;

        public AppUser User { get; set; } 

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
		public int? PromoCodeId { get; set; }  // nullable, optional
		public PromoCode PromoCode { get; set; }

		public ICollection<OrderItem> OrderItems { get; set; } = [];

        public Payment? Payment { get; set; }
    }
}
