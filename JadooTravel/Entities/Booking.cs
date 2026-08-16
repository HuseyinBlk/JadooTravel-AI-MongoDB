using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JadooTravel.Entities;

public class Booking
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string BookingId { get; set; }
    public string DestinationId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime? TravelDate { get; set; }
    public int GuestsCount { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}