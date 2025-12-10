using Microsoft.AspNetCore.Http;

namespace Nois.Application.Exceptions
{
    public class EmailAlreadyTaken : BusinessException
    {
        public EmailAlreadyTaken() : base("This email address is already being used.", StatusCodes.Status409Conflict) { }
    }
}
