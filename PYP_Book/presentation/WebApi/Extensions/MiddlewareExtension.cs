using Microsoft.AspNetCore.Builder;
using WebApi.Middlewares;

namespace WebApi.Extensions
{
    public static  class MiddlewareExtension
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
       
    }
}
