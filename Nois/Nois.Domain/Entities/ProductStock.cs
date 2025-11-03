using Nois.Domain.Entities.Common;

namespace Nois.Domain.Entities
{
    public class ProductStock:BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public int SizeId { get; set; }
        public Size Size { get; set; } = default!;

        public int ColorId { get; set; }
        public Color Color { get; set; } = default!;

        public int Quantity { get; set; }
    }
}
