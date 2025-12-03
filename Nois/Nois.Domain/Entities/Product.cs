using Nois.Domain.Entities.Common;

namespace Nois.Domain.Entities
{
    public class Product:AuditableEntity
    {
        public string BlobName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductVariant> ProductVariants { get; set; } = new();
    }
}
