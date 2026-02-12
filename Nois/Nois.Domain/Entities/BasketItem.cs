using Nois.Domain.Entities.Common;

namespace Nois.Domain.Entities
{
    public class BasketItem : BaseEntity
    {
        public int BasketId { get; set; }
        public Basket Basket { get; set; } = null!;

        public int ProductVariantId { get; set; }

        // Snapshot for UI & safety
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}
