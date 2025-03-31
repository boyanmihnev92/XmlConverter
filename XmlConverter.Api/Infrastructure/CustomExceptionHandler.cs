using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using XmlConverter.Application.Common.Exceptions;

namespace XmlConverter.Api.Infrastructure
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers = new()
        {
            {typeof(CustomValidationException), HandleValidationException },
        };

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionType = exception.GetType();

            if (!_exceptionHandlers.TryGetValue(exceptionType, out var handler))
            {
                var func = HandleAnyException;
                await func.Invoke(httpContext, exception);

                return true;
            }

            await handler.Invoke(httpContext, exception);

            return true;

        }

        private static Task HandleValidationException(HttpContext httpContext, Exception ex)
        {
            var exception = (CustomValidationException)ex;

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return httpContext.Response.WriteAsJsonAsync(new ValidationProblemDetails(exception.Errors)
            {
                Status = StatusCodes.Status400BadRequest,
            });
        }

        private static Task HandleAnyException(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Unhandled Exception occured.",
                Detail = exception.Message
            });
        }
    }
}
