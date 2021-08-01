using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebApiExample.Middlewares
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //Adding value to header
            //httpContext.Response.Headers.Add("sth", "s1234");
            try
            {
                await _next(httpContext);
            }
            catch (Exception exc)
            {
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync("Unexpected problem!");
            }
        }

    }
}
