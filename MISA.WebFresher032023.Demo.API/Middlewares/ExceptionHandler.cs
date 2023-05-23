using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;

namespace MISA.WebFresher032023.Demo.API.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            string exceptionText;
            if (exception is BaseException baseException)
            {
                exceptionText = new ExceptionResponse()
                {
                    ErrorCode = baseException.ErrorCode,
                    UserMessage = baseException.UserMessage,
                    DevMessage = baseException.Message,
                    TraceId = httpContext.TraceIdentifier
                }.ToString();

                if (exception is InternalException)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                } else if (exception is NotFoundException)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                } else if (exception is DbException)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                } else if (exception is ConflictException)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                }


            } else
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                exceptionText = new ExceptionResponse()
                {
                    ErrorCode = Error.ServerFailed,
                    UserMessage = Error.ServerFailedMessage,
                    DevMessage = exception.Message,
                    TraceId = httpContext.TraceIdentifier

                }.ToString();
            }

            await httpContext.Response.WriteAsync(exceptionText);
        }

    }
}
