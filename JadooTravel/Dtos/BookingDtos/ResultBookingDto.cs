namespace JadooTravel.Dtos.BookingDtos;

public class ResultBookingDto
{
    public string BookingId { get; set; }
    public string DestinationId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime? TravelDate { get; set; }
    public int GuestsCount { get; set; }
    public DateTime CreatedDate { get; set; }
}