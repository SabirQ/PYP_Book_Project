using WebApi.Models;

namespace WebApi.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
                //await _next.Invoke(context);
            }
        }

        public async Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var model = new ExceptionModel
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            };

            await context.Response.WriteAsync(model.ToString());
        }
    }
}
