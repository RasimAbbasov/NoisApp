using Microsoft.AspNetCore.Http;

namespace Nois.Application.Exceptions
{
    public class InvalidCredentialsException : BusinessException
    {
        public InvalidCredentialsException() : base("Invalid email or password.", StatusCodes.Status401Unauthorized) { }
    }
}
