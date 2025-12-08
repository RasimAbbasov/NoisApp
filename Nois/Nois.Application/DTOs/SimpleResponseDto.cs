namespace Nois.Application.DTOs
{
    public class SimpleResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public SimpleResponseDto(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
