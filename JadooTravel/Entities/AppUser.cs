using AspNetCore.Identity.MongoDbCore.Models;

namespace JadooTravel.Entities;

public class AppUser : MongoIdentityUser<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}