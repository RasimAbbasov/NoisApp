using Microsoft.AspNetCore.Http;

namespace Nois.Application.Exceptions
{
    public class LoginDisabledException : BusinessException
    {
        public LoginDisabledException() : base("Login is currently disabled for this account.",StatusCodes.Status403Forbidden) { }
    }
}
