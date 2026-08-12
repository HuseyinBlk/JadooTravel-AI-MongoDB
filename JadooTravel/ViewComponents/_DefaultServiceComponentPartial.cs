using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents;

public class _DefaultServiceComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}