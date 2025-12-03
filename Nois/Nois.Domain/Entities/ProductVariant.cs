using Nois.Domain.Entities.Common;

namespace Nois.Domain.Entities
{
    public class ProductVariant : AuditableEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public int SizeId { get; set; }
        public Size Size { get; set; } = default!;

        public int ColorId { get; set; }
        public Color Color { get; set; } = default!;

        public ProductStock ProductStock { get; set; } = new() { Quantity = 0 };

    }
}
