using Nois.Domain.Entities.Common;
using Nois.Domain.Entities.Identity;

namespace Nois.Domain.Entities
{
    public class Wishlist : AuditableEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
