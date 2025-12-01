namespace Nois.Application.DTOs.ProductDtos
{
    public class ProductSummaryDto
    {
        public int Id { get; set; }
        public string BlobName { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }   
    }
}
