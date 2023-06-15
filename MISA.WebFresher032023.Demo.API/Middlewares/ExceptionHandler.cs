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
        /// <summary>
        /// Hàm bắt sự kiện của middleware
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)
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

        /// <summary>
        /// Xử lý khi nhận được exception
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)
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
            // Trường hợp lỗi chung chung, không xác định được kiểu của Exception
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                exceptionText = new ExceptionResponse()
                {
                    ErrorCode = Error.ServerFailed,
                    UserMessage = Error.ServerFailedMsg,
                    DevMessage = exception.Message,
                    TraceId = httpContext.TraceIdentifier

                }.ToString();
            }

            await httpContext.Response.WriteAsync(exceptionText);
        }

    }
}
