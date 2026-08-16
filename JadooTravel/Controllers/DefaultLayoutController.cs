using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

public class DefaultLayoutController : Controller
{
    public IActionResult Layout()
    {
        return View();
    }
}