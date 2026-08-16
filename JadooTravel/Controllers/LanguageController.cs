using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

public class LanguageController : Controller
{
    [HttpGet("/Language/Change/{lang}")]
    public IActionResult ChangeLanguage(string lang)
    {
        var cookieOptions = new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddYears(1),
            HttpOnly = true,
            Secure = Request.IsHttps, 
            SameSite = SameSiteMode.Lax
        };
        
        Response.Cookies.Append("SelectedLanguage", lang.ToUpper(), cookieOptions);
        
        var referer = Request.Headers["Referer"].ToString();
        if (!string.IsNullOrEmpty(referer))
        {
            return Redirect(referer);
        }
        return Redirect("/Default/Index");
    }
}