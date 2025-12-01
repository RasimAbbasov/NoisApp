

namespace Nois.Application.DTOs.ProductVariantDtos
{
    public class ProductVariantSummaryDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
    }
}
