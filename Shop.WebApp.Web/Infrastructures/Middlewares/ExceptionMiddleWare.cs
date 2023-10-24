using Newtonsoft.Json;
using Shop.Webapp.Shared.Exceptions;
using Shop.Webapp.Shared.Results;

namespace Shop.WebApp.Web.Infrastructures.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            try
            {
                await _next(httpContext);
            }
            catch (CustomerException ex)
            {
                _logger.LogError(ex, "Error customer");
                httpContext.Response.StatusCode = (int)ex.StatusCode;
                var result = JsonConvert.SerializeObject(new ErrorResult(ex.Message, ex.Errors));
                await httpContext.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "System error");
                httpContext.Response.StatusCode = 500;
                var result = JsonConvert.SerializeObject(new ErrorResult("Server Internal"));
                await httpContext.Response.WriteAsync(result);
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddleWareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
