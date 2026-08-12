using JadooTravel.Dtos.DestinationDtos;
using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

public class DestinationController(IDestinationService _destinationService) : Controller
{
    // GET
    public async Task<IActionResult> DestinationList()
    {
        var values = await _destinationService.GetAllDestinationsAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateDestination()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CreateDestination(CreateDestinationDto createDestinationDto)
    {
        var values = _destinationService.CreateDestinationAsync(createDestinationDto);
        return RedirectToAction("DestinationList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateDestination(string id)
    {
        var value = await _destinationService.GetDestinationByIdAsync(id);
        return View(value);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateDestination(UpdateDestinationDto updateDestinationDto)
    {
        await _destinationService.UpdateDestinationAsync(updateDestinationDto);
        return RedirectToAction("DestinationList");
    }

    public async Task<IActionResult> DeleteDestination(string id)
    {
        await _destinationService.DeleteDestinationAsync(id);
        return RedirectToAction("DestinationList");
    }
}