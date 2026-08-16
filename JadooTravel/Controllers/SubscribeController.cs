using JadooTravel.Dtos.SubscribeDtos;
using JadooTravel.Services.SubscribeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

[Authorize]
[Route("/Admin/Subscribe")]
public class SubscribeController(ISubscribeService subscribeService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> SubscribeList()
    {
        var values = await subscribeService.GetAllSubscribeAsync();
        return View(values);
    }

    [HttpPost("/Subscribe/NewSubscribe")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> NewSubscribe(CreateSubscribeDto createSubscribeDto)
    {
        await subscribeService.CreateSubscribeAsync(createSubscribeDto);
        return Redirect("/Default/Index");
    }

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> DeleteSubscribe(string id)
    {
        await subscribeService.DeleteSubscribeAsync(id);
        return RedirectToAction("SubscribeList");
    }
}