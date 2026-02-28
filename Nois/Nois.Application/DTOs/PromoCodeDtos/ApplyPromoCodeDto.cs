using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Application.DTOs.PromoCodeDtos
{
	public class ApplyPromoCodeDto
	{
		public string Code { get; set; }
		public decimal OrderAmount { get; set; }
		public string BuyerId { get; set; }
	}
}
