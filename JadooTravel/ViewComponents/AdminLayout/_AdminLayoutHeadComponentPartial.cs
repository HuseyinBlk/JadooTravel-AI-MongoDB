using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents.AdminLayout;

public class _AdminLayoutHeadComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}