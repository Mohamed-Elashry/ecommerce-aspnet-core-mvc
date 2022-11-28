using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace E_Commerce.Middlewares
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var currentCulture = context.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
                var browserCulture = context.Request.Headers["Accept-Language"].ToString()[..2];
                if (string.IsNullOrEmpty(currentCulture))
                {
                    var culture = browserCulture == "ar" ? "ar-EG" :
                                  browserCulture == "de" ? "de-DE" :
                                  "en-US";
                    // To set changed or selected value in dropdown list of languages in layout
                    var requestCulture = new RequestCulture(culture, culture);
                    context.Features.Set<IRequestCultureFeature>(new RequestCultureFeature(requestCulture, null));

                    CultureInfo.CurrentCulture = new(culture);
                    CultureInfo.CurrentUICulture = new(culture);
                }
                await _next(context);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
