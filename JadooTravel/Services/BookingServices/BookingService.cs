using AutoMapper;
using JadooTravel.Dtos.BookingDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.BookingServices;

public class BookingService(IMongoDatabase database, IMapper _mapper, IDatabaseSettings databaseSettings) : IBookingService
{
    private readonly IMongoCollection<Booking> _bookingCollection = database.GetCollection<Booking>(databaseSettings.BookingCollectionName);

    public async Task<List<ResultBookingDto>> GetAllBookingsAsync()
    {
        var values = await _bookingCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultBookingDto>>(values);
    }

    public async Task CreateBookingAsync(CreateBookingDto bookingDto)
    {
        var value = _mapper.Map<Booking>(bookingDto);
        await _bookingCollection.InsertOneAsync(value);
    }

    public async Task DeleteBookingAsync(string id)
    {
        await _bookingCollection.DeleteOneAsync(x => x.BookingId == id);
    }
}