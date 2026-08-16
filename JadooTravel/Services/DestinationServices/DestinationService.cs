using AutoMapper;
using JadooTravel.Dtos.DestinationDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.DestinationServices;

public class DestinationService(IMongoDatabase database, IMapper mapper, IDatabaseSettings databaseSettings ) : IDestinationService
{
    private readonly IMongoCollection<Destination> _destinationCollection = database.GetCollection<Destination>(databaseSettings.DestinationCollectionName);
    
    public async Task<List<ResultDestinationDto>> GetAllDestinationsAsync()
    {
        var destinations = await _destinationCollection.Find(x => true).ToListAsync();
        return mapper.Map<List<ResultDestinationDto>>(destinations);
    }

    public async Task CreateDestinationAsync(CreateDestinationDto destinationDto)
    {
        var value = mapper.Map<Destination>(destinationDto);
        await _destinationCollection.InsertOneAsync(value);
    }

    public async Task UpdateDestinationAsync(UpdateDestinationDto destinationDto)
    {
        var value = mapper.Map<Destination>(destinationDto);
        await _destinationCollection.FindOneAndReplaceAsync(x => x.DestinationId == destinationDto.DestinationId, value);
    }

    public async Task DeleteDestinationAsync(string id)
    {
        await _destinationCollection.DeleteOneAsync(x => x.DestinationId == id);
    }

    public async Task<GetDestinationByIdDto> GetDestinationByIdAsync(string id)
    {
        var value = await _destinationCollection.Find(x => x.DestinationId == id).FirstOrDefaultAsync();
        return mapper.Map<GetDestinationByIdDto>(value);
    }
}