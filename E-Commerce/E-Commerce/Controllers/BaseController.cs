using Microsoft.AspNetCore.Localization;
using System.ComponentModel;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public string Language = "en-US";
        [AllowAnonymous]
        public IActionResult SetLanguage(string culture, string ReturnURL)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            Language = culture;
            return LocalRedirect(ReturnURL);
        }
        
        public class SupportedLanguage 
        {
            public static string English = "en-US";
            public static string Arabic = "ar-EG";
            public static string SaudiaArabia = "ar-SA";
            public static string French = "de-DE";
        };
    }
    
}
