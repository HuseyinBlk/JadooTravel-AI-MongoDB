using JadooTravel.Entities;
using JadooTravel.Services.BookingServices;
using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents._AdminLayout;

public class _AdminLayoutHeaderComponentPartial(
    UserManager<AppUser> userManager,
    IBookingService bookingService,
    IDestinationService destinationService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await userManager.GetUserAsync(Request.HttpContext.User);

        var bookings = await bookingService.GetAllBookingsAsync();
        var destinations = await destinationService.GetAllDestinationsAsync();
        var destDict = destinations.ToDictionary(d => d.DestinationId, d => d);

        var latestBookings = bookings
            .OrderByDescending(b => b.CreatedDate)
            .Take(3)
            .Select(b => new
            {
                b.FullName,
                City = destDict.TryGetValue(b.DestinationId, out var dest) ? dest.City : "Rota",
                b.CreatedDate
            })
            .ToList();

        ViewBag.NotificationCount = latestBookings.Count;
        ViewBag.LatestNotifications = latestBookings;

        return View(user);
    }
}