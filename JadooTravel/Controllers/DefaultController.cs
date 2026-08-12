using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

public class DefaultController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}