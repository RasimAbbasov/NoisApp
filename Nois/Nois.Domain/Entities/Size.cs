using Nois.Domain.Entities.Common;

namespace Nois.Domain.Entities
{
    public class Size: AuditableEntity
    {
        public string Code { get; set; } = "";   // “M”
        public string Name { get; set; } = "";   // “Medium”
        public int SortOrder { get; set; }       // to sort S<M<L<XL
        public List<ProductVariant> Stocks { get; set; }
    }
}
