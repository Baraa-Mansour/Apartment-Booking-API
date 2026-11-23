using API.Common;
using BLL.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace API.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _env;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (statusCode, message) = exception switch
            {
                NotFoundException e => (StatusCodes.Status404NotFound, e.Message),
                ConflictException e => (StatusCodes.Status409Conflict, e.Message),
                ForbiddenException e => (StatusCodes.Status403Forbidden, e.Message),
                UnauthorizedException e => (StatusCodes.Status401Unauthorized, e.Message),
                InvalidOperationException e => (StatusCodes.Status400BadRequest, e.Message),
                _ => (StatusCodes.Status500InternalServerError,
                                                "An unexpected error occurred.")
            };

            if (statusCode >= 500)
                _logger.LogError(exception, "Unhandled exception on {Path}", httpContext.Request.Path);
            else
                _logger.LogWarning("Handled {Type} on {Path}: {Message}",
                    exception.GetType().Name, httpContext.Request.Path, exception.Message);

            // Only show internals for unexpected errors, and only in Development
            var details = _env.IsDevelopment() && statusCode >= 500 ? exception.ToString() : null;

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(
                ApiResponse<object>.Fail(message, details), cancellationToken);

            return true; // exception handled, stop the pipeline
        }
    }
}
