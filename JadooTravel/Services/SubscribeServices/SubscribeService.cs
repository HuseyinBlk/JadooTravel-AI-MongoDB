using AutoMapper;
using JadooTravel.Dtos.SubscribeDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.SubscribeServices;

public class SubscribeService(IMongoDatabase database, IMapper _mapper, IDatabaseSettings databaseSettings) : ISubscribeService
{
    private readonly IMongoCollection<Subscribe>  _subscribeCollection = database.GetCollection<Subscribe>(databaseSettings.SubscribeCollectionName);
    
    public async Task<List<ResultSubscribeDto>> GetAllSubscribeAsync()
    {
        var values = await _subscribeCollection.Find(x=> true).ToListAsync();
        return _mapper.Map<List<ResultSubscribeDto>>(values);
    }

    public async Task CreateSubscribeAsync(CreateSubscribeDto subscribeDto)
    {
        var value = _mapper.Map<Subscribe>(subscribeDto);
        await _subscribeCollection.InsertOneAsync(value);
    }

    public async Task DeleteSubscribeAsync(string id)
    {
        await _subscribeCollection.DeleteOneAsync(x => x.SubscribeId == id);
    }
}