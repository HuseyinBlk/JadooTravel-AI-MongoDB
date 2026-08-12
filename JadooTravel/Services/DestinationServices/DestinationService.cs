using AutoMapper;
using JadooTravel.Dtos.DestinationDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.DestinationServices;

public class DestinationService : IDestinationService
{
    private readonly IMongoCollection<Destination> _destinationCollection;
    private readonly IMapper _mapper;

    public DestinationService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _destinationCollection = database.GetCollection<Destination>(databaseSettings.DestinationCollectionName);
        _mapper = mapper;
    }
    
    public async Task<List<ResultDestinationDto>> GetAllDestinationsAsync()
    {
        var destinations = await _destinationCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultDestinationDto>>(destinations);
    }

    public async Task CreateDestinationAsync(CreateDestinationDto destinationDto)
    {
        var value = _mapper.Map<Destination>(destinationDto);
        await _destinationCollection.InsertOneAsync(value);
    }

    public async Task UpdateDestinationAsync(UpdateDestinationDto destinationDto)
    {
        var value = _mapper.Map<Destination>(destinationDto);
        await _destinationCollection.FindOneAndReplaceAsync(x => x.DestinationId == destinationDto.DestinationId, value);
    }

    public async Task DeleteDestinationAsync(string id)
    {
        await _destinationCollection.DeleteOneAsync(x => x.DestinationId == id);
    }

    public async Task<GetDestinationByIdDto> GetDestinationByIdAsync(string id)
    {
        var value = await _destinationCollection.Find(x => x.DestinationId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetDestinationByIdDto>(value);
    }
}