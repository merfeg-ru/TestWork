using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Receiver.Exceptions;


namespace Receiver.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogInformation(ex.Message);
                await HandleExceptionAsync(context, logger, ex);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Произошло необработанное исключение");
                await HandleExceptionAsync(context, logger, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, ILogger log, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new { error = exception.Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
