using Domain.Exceptions;

namespace ChessTournament.Middleware
{
    public class ErrorHandler : IMiddleware
    {
        private readonly ILogger<ErrorHandler> _errorLogger;

        public ErrorHandler(ILogger<ErrorHandler> errorLogger)
        {
            _errorLogger = errorLogger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException e)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(e.Message);
            }
            catch (Exception e)
            {
                _errorLogger.LogError(e, "Internal server error status code has been returned.");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
