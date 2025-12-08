namespace Nois.Application.DTOs
{
    public class GenericResponseDto<T> // for responses with data
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public GenericResponseDto(int statusCode, string message, T data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
