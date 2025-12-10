using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Nois.Application.Exceptions
{
    public class BusinessExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public BusinessExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                var problem = new ProblemDetails
                {
                    Status = ex.StatusCode,
                    Title = ex.Message,
                    Type = ex.GetType().Name
                };

                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(problem);

                return; // very important!
            }
            catch (Exception)
            {
                throw;
            }

        }

    }

}
