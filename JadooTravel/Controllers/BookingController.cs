using JadooTravel.Dtos.BookingDtos;
using JadooTravel.Services.BookingServices;
using JadooTravel.Services.DestinationServices;
using JadooTravel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

[Authorize]
[Route("/Admin/Booking")]
public class BookingController(IBookingService bookingService, IDestinationService destinationService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var bookings = await bookingService.GetAllBookingsAsync();
        var destinations = await destinationService.GetAllDestinationsAsync();

        var viewModelList = bookings.Select(b => new BookingViewModel
        {
            Booking = b,
            Destination = destinations.FirstOrDefault(d => d.DestinationId == b.DestinationId)
        }).ToList();
        
        return View(viewModelList);
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> MakeReservation(CreateBookingDto bookingDto)
    {
        await bookingService.CreateBookingAsync(bookingDto);
        return RedirectToAction("Index", "Default");
    }

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> DeleteBooking(string id)
    {
        await bookingService.DeleteBookingAsync(id);
        return RedirectToAction("Index", "Booking");
    }
}