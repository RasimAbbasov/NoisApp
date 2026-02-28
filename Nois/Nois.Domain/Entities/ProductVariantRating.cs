using Nois.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Domain.Entities
{
	public class ProductVariantRating: AuditableEntity
	{
		public int ProductVariantId { get; set; }
		public ProductVariant ProductVariant { get; set; }

		public string UserId { get; set; }

		[Range(1, 5)]
		public int Stars { get; set; }
		public string? Comment { get; set; }
	}

}
