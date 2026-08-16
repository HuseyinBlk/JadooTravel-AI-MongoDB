using JadooTravel.Dtos.SubscribeDtos;

namespace JadooTravel.Services.SubscribeServices;

public interface ISubscribeService
{
    Task<List<ResultSubscribeDto>> GetAllSubscribeAsync();
    Task CreateSubscribeAsync(CreateSubscribeDto subscribeDto);
    Task DeleteSubscribeAsync(string id);
}