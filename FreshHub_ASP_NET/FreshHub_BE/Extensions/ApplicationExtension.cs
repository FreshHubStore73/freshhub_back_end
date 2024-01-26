using FreshHub_BE.Middleware;

namespace FreshHub_BE.Extensions
{
    public static class ApplicationExtension
    {
        public static void UseExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
