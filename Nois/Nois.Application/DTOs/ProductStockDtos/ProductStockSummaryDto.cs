
namespace Nois.Application.DTOs.ProductStockDtos
{
    public class ProductStockSummaryDto
    {
        public int Id { get; set; }
        public int ProductVariantId { get; set; }
        public string ProductName { get; set; }
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        public int Quantity { get; set; }
    }
}
