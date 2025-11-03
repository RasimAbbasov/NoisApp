using Nois.Domain.Entities.Common;

namespace Nois.Domain.Entities
{
    public class Color:AuditableEntity
    {
        public string Code { get; set; } = default!;   // "BLK"
        public string Name { get; set; } = default!;   // "Black"
        public int SortOrder { get; set; }
    }
}
