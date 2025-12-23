using Nois.Domain.Entities.Common;

namespace Nois.Domain.Entities
{
    public class Basket : BaseEntity
    {
        // One basket per user
        public string BuyerId { get; set; } = string.Empty;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Optimistic concurrency
        public byte[] RowVersion { get; set; } = [];

        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
