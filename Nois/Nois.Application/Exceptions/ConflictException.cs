

using Microsoft.AspNetCore.Http;

namespace Nois.Application.Exceptions
{
    public class ConflictException : BusinessException
    {
        public ConflictException(string message) : base(message,StatusCodes.Status409Conflict) { }
       
    }
}

