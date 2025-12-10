using Microsoft.AspNetCore.Http;

namespace Nois.Application.Exceptions
{
    public class UserLockedOutException : BusinessException
    {
        public UserLockedOutException() : base("User account is temporarily locked out due to too many failed login attempts.", StatusCodes.Status423Locked) { }
    }
}
