using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Application.DTOs.PromoCodeDtos
{
	public class ApplyPromoCodeResultDto
	{
		public bool IsValid { get; set; }
		public string Message { get; set; }
		public decimal DiscountAmount { get; set; }
		public int? PromoCodeId { get; set; }
	}
}
