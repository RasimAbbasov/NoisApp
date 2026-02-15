using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Application.DTOs.ProductVariantRatingDtos
{
    public class ProductVariantRatingDto
    {
		public int Id {  get; set; }
		public int ProductVariantId { get; set; }
		public string UserId { get; set; }
		public int Stars { get; set; }
	}
}
