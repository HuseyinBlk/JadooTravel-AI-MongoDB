using JadooTravel.Dtos.BookingDtos;

namespace JadooTravel.Services.BookingServices;

public interface IBookingService
{
    Task<List<ResultBookingDto>> GetAllBookingsAsync();
    Task CreateBookingAsync(CreateBookingDto bookingDto);
    Task DeleteBookingAsync(string id);
}