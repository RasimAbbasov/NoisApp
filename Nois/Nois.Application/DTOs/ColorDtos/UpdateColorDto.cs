namespace Nois.Application.DTOs.ColorDtos
{
    public class UpdateColorDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;  
        public int SortOrder { get; set; }
    }
}
