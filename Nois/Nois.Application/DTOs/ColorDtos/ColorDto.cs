namespace Nois.Application.DTOs.ColorDtos
{
    public class ColorDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;   // "BLK"
        public string Name { get; set; } = default!;   // "Black"
        public int SortOrder { get; set; }
    }
}
