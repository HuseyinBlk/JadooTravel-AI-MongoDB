using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JadooTravel.Entities;

public class Testimonial
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string TestimonialId { get; set; }
    public string PhotoUrl  { get; set; }
    public string NameSurname { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
}