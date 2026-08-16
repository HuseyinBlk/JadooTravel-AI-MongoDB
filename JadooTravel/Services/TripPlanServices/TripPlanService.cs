using AutoMapper;
using JadooTravel.Dtos.TripPlanDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.TripPlanServices;

public class TripPlanService(IMongoDatabase database, IMapper mapper, IDatabaseSettings databaseSettings) : ITripPlanService
{
    private readonly IMongoCollection<TripPlan> _tripPlanCollection= database.GetCollection<TripPlan>(databaseSettings.TripPlanCollectionName);
    
    public async Task<List<ResultTripPlanDto>> GetAllTripPlansAsync()
    {
        var values = await _tripPlanCollection.Find(x => true).ToListAsync();
        return mapper.Map<List<ResultTripPlanDto>>(values);
    }

    public async Task CreateTripPlanAsync(CreateTripPlanDto tripPlanDto)
    {
        var tripPlan = mapper.Map<TripPlan>(tripPlanDto);
        await _tripPlanCollection.InsertOneAsync(tripPlan);
    }

    public async Task UpdateTripPlanAsync(UpdateTripPlanDto tripPlanDto)
    {
        var tripPlan = mapper.Map<TripPlan>(tripPlanDto);
        await _tripPlanCollection.FindOneAndReplaceAsync(x=> x.TripPlanId == tripPlanDto.TripPlanId, tripPlan);
    }

    public async Task DeleteTripPlanAsync(string id)
    {
        await  _tripPlanCollection.DeleteOneAsync(x=>x.TripPlanId == id);
    }

    public async Task<GetTripPlanByIdDto> GetTripPlanByIdAsync(string id)
    {
        var tripPlan = await _tripPlanCollection.Find(x=> x.TripPlanId == id).FirstOrDefaultAsync();
        return mapper.Map<GetTripPlanByIdDto>(tripPlan);
    }
}