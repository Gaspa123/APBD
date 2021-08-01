using Microsoft.AspNetCore.Builder;

namespace WebApiExample.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGreatErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorLoggingMiddleware>();
        }
    }
}
