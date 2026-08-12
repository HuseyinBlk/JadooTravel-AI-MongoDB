using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents;

public class _DefaultBookingComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}