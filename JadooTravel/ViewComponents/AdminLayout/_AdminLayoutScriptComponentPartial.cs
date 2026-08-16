using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents.AdminLayout;

public class _AdminLayoutScriptComponentPartial: ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}