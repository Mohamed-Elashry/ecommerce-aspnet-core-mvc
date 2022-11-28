using Microsoft.AspNetCore.Localization;

namespace E_Commerce.Controllers
{
    public class BaseController : Controller
    {
       public IActionResult SetLanguage(string culture, string ReturnURL)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            return LocalRedirect(ReturnURL);
        }
    }
}
