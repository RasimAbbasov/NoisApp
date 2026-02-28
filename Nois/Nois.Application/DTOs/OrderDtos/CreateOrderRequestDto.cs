using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Application.DTOs.OrderDtos
{
	public class CreateOrderRequestDto
	{
		public string BuyerId { get; set; }
		public string? PromoCode { get; set; }
	}
}
