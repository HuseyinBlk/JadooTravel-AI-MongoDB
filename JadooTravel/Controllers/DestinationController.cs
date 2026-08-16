using JadooTravel.Dtos.DestinationDtos;
using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

[Authorize]
[Route("/Admin/Destination")]
public class DestinationController(IDestinationService destinationService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> DestinationList()
    {
        var values = await destinationService.GetAllDestinationsAsync();
        return View(values);
    }

    [HttpGet("Create")]
    public IActionResult CreateDestination()
    {
        return View();
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> CreateDestination(CreateDestinationDto createDestinationDto)
    {
        await destinationService.CreateDestinationAsync(createDestinationDto);
        return RedirectToAction("DestinationList");
    }

    [HttpGet("Update/{id}")]
    public async Task<IActionResult> UpdateDestination(string id)
    {
        var value = await destinationService.GetDestinationByIdAsync(id);
        return View(value);
    }

    [HttpPost("Update/{id}")]
    public async Task<IActionResult> UpdateDestination(UpdateDestinationDto updateDestinationDto)
    {
        await destinationService.UpdateDestinationAsync(updateDestinationDto);
        return RedirectToAction("DestinationList");
    }

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> DeleteDestination(string id)
    {
        await destinationService.DeleteDestinationAsync(id);
        return RedirectToAction("DestinationList");
    }
}