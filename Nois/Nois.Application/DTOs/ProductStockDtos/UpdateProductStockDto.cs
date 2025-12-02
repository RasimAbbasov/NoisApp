
namespace Nois.Application.DTOs.ProductStockDtos
{
    public class UpdateProductStockDto
    {
        public int Id { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}
