using JadooTravel.Services.BookingServices;
using JadooTravel.Services.DestinationServices;
using JadooTravel.Services.TripPlanServices;
using JadooTravel.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents;

public class _DefaultBookingComponentPartial(ITripPlanService _tripPlanService, IDestinationService _destinationService ) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var tripPlans = await _tripPlanService.GetAllTripPlansAsync();
        var destinations = await _destinationService.GetAllDestinationsAsync();

        var model = new DefaultBookingViewModel
        {
            Destinations = destinations,
            TripPlans = tripPlans
        };
        return View(model);
    }
}