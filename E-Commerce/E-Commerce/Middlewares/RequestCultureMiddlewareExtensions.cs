namespace E_Commerce.Middlewares
{
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}
