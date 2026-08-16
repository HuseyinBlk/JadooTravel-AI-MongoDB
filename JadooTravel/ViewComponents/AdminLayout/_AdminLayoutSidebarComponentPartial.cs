using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents.AdminLayout;

public class _AdminLayoutSidebarComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
    
}