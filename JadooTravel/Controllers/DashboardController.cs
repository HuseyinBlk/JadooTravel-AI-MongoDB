using JadooTravel.Services.BookingServices;
using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Linq;

namespace JadooTravel.Controllers;

[Authorize]
[Route("/Admin/Dashboard")]
public class DashboardController(IBookingService bookingService, IDestinationService destinationService) : Controller
{
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var bookings = await bookingService.GetAllBookingsAsync();
        var destinations = await destinationService.GetAllDestinationsAsync();
        
        var destDict = destinations.ToDictionary(d => d.DestinationId, d => d);

        var weeklyBookings = new int[7];
        var weeklyGuests = new int[7];
        
        foreach (var booking in bookings)
        {
            int dayOfWeekIndex = ((int)booking.CreatedDate.DayOfWeek + 6) % 7; 
            weeklyBookings[dayOfWeekIndex]++;
            weeklyGuests[dayOfWeekIndex] += booking.GuestsCount;
        }

        var destGroups = bookings
            .GroupBy(b => b.DestinationId)
            .Select(g => new
            {
                City = destDict.TryGetValue(g.Key, out var dest) ? $"{dest.City}, {dest.Country}" : "Bilinmeyen Rota",
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ToList();

        var donutLabels = destGroups.Select(x => x.City).ToArray();
        var donutSeries = destGroups.Select(x => x.Count).ToArray();
        
        if (donutLabels.Length == 0)
        {
            donutLabels = ["Rezervasyon Yok"];
            donutSeries = [0];
        }

        var totalBookingsCount = bookings.Count;

        decimal totalRevenue = 0;
        var dailyRevenues = new Dictionary<DateTime, decimal>();

        for (int i = 6; i >= 0; i--)
        {
            dailyRevenues[DateTime.Today.AddDays(-i)] = 0;
        }

        foreach (var booking in bookings)
        {
            decimal price = destDict.TryGetValue(booking.DestinationId, out var dest) ? dest.Price : 0;
            decimal revenue = booking.GuestsCount * price;
            totalRevenue += revenue;

            var date = booking.CreatedDate.Date;
            if (dailyRevenues.ContainsKey(date))
            {
                dailyRevenues[date] += revenue;
            }
        }

        var sparklineSeries = dailyRevenues.OrderBy(x => x.Key).Select(x => (double)x.Value).ToArray();

        var recentBookings = bookings
            .OrderByDescending(b => b.CreatedDate)
            .Take(5)
            .Select(b => new
            {
                b.FullName,
                b.Email,
                b.Phone,
                City = destDict.TryGetValue(b.DestinationId, out var dest) ? dest.City : "Bilinmeyen Rota",
                Price = destDict.TryGetValue(b.DestinationId, out var dest2) ? dest2.Price : 0,
                b.GuestsCount,
                b.TravelDate,
                b.CreatedDate
            })
            .ToList();

        var popularTours = destinations
            .Take(4)
            .ToList();

        ViewBag.WeeklyBookings = JsonSerializer.Serialize(weeklyBookings);
        ViewBag.WeeklyGuests = JsonSerializer.Serialize(weeklyGuests);
        ViewBag.DonutLabels = JsonSerializer.Serialize(donutLabels);
        ViewBag.DonutSeries = JsonSerializer.Serialize(donutSeries);
        ViewBag.TotalBookingsCount = totalBookingsCount;
        ViewBag.TotalRevenue = totalRevenue.ToString("C0", new System.Globalization.CultureInfo("en-US"));
        ViewBag.SparklineSeries = JsonSerializer.Serialize(sparklineSeries);
        ViewBag.RecentBookings = recentBookings;
        ViewBag.PopularTours = popularTours;

        return View();
    }
}