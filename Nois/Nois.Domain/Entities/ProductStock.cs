using Nois.Domain.Entities.Common;

namespace Nois.Domain.Entities
{
    public class ProductStock : AuditableEntity
    {
        public int ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; } = default!;
        public int Quantity { get; set; }
    }
}
