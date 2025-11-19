using Microsoft.AspNetCore.Http;

namespace Nois.Application.DTOs.ProductDtos
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }
}
