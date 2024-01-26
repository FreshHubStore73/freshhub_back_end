using FreshHub_BE.Exception;

namespace FreshHub_BE.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> loger;
        private readonly IHostEnvironment hostEnvironment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> loger, IHostEnvironment hostEnvironment)
        {
            this.next = next;
            this.loger = loger;
            this.hostEnvironment = hostEnvironment;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (System.Exception ex)
            {
                loger.LogError(ex, ex.Message);

                ApiException apiException = null;

                if (hostEnvironment.IsDevelopment())
                {
                    apiException = new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString());
                }
                else
                {
                    apiException = new ApiException(context.Response.StatusCode, ex.Message, "Internal server error.");
                }
                await context.Response.WriteAsJsonAsync(apiException);
            }

        }


    }
}
