using Nois.Domain.Entities.Common;

namespace Nois.Domain.Entities
{
    public class Category:AuditableEntity
    {
      public string Name { get; set; }
      public List<Product> Products { get; set; }
    }
}
