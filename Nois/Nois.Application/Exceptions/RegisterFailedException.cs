using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Application.Exceptions
{
    public class RegisterFailedException : BusinessException
    {
        public RegisterFailedException(IEnumerable<IdentityError> errors)
            : base(string.Join("; ", errors.Select(e => e.Description)),
                   StatusCodes.Status400BadRequest)
        {
        }
    }
}
