using Nois.Domain.Entities.Common;

namespace Nois.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        // Financial snapshot
        public decimal PriceAtPurchase { get; set; }
    }
}
