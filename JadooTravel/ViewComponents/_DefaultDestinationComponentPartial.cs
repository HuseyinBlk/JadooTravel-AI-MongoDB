using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents;

public class _DefaultDestinationComponentPartial(IDestinationService _destinationService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _destinationService.GetAllDestinationsAsync();
        return View(values);
    }
}