using JadooTravel.Dtos.BookingDtos;
using JadooTravel.Dtos.DestinationDtos;

namespace JadooTravel.ViewModels;

public class BookingViewModel
{
    public ResultBookingDto Booking { get; set; }
    public ResultDestinationDto Destination { get; set; }
}