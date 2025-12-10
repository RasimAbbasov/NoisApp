using Microsoft.AspNetCore.Http;

namespace Nois.Application.Exceptions
{
    public  class EmailNotConfirmedException : BusinessException
    {
        public EmailNotConfirmedException() : base("User email address has not been confirmed.",StatusCodes.Status403Forbidden) { }
    }
}
