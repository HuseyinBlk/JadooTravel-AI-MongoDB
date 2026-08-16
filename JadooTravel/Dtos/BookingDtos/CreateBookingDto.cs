namespace JadooTravel.Dtos.BookingDtos;

public class CreateBookingDto
{
    public string DestinationId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime? TravelDate { get; set; }
    public int GuestsCount { get; set; }
}