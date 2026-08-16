using JadooTravel.Dtos.TripPlanDtos;

namespace JadooTravel.Services.TripPlanServices;

public interface ITripPlanService
{
    Task<List<ResultTripPlanDto>> GetAllTripPlansAsync();
    Task CreateTripPlanAsync(CreateTripPlanDto tripPlanDto);
    Task UpdateTripPlanAsync(UpdateTripPlanDto tripPlanDto);
    Task DeleteTripPlanAsync(string id);
    Task<GetTripPlanByIdDto> GetTripPlanByIdAsync(string id);
}