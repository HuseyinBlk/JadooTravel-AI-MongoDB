using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

[Authorize]
public class AdminLayoutController : Controller
{
    public IActionResult Layout()
    {
        return View();
    }
}