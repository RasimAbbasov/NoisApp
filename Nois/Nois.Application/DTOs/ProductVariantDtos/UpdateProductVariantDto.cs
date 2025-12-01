
namespace Nois.Application.DTOs.ProductVariantDtos
{
    public class UpdateProductVariantDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
    }
}
