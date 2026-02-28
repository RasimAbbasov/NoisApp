using Nois.Domain.Entities.Common;

namespace Nois.Domain.Entities
{
    public class PromoCode :AuditableEntity
    {
		public string Code { get; set; }              // "CODE20"
		public decimal DiscountAmount { get; set; }   // fixed discount
		public decimal DiscountPercent { get; set; }  // percentage discount (0-100)
		public decimal MinOrderAmount { get; set; }  // minimum order value to use
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int MaxUsage { get; set; }            // how many times this promo can be used in total
		public int UsedCount { get; set; }           // how many times it has been used
		public bool IsActive { get; set; }

		public List<Order> Orders { get; set; } = new List<Order>();
	}
}
