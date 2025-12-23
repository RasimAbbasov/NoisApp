using Nois.Domain.Entities.Common;
using Nois.Domain.Entities.Enums;

namespace Nois.Domain.Entities
{
    public class Order : BaseEntity
    {

        public string BuyerId { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public ICollection<OrderItem> OrderItems { get; set; } = [];

        public Payment? Payment { get; set; }
    }
}
