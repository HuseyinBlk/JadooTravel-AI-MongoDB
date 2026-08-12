using JadooTravel.Dtos.DestinationDtos;

namespace JadooTravel.Services.DestinationServices;

public interface IDestinationService
{
    Task<List<ResultDestinationDto>> GetAllDestinationsAsync();
    Task CreateDestinationAsync(CreateDestinationDto destinationDto);
    Task UpdateDestinationAsync(UpdateDestinationDto destinationDto);
    Task DeleteDestinationAsync(string id);
    Task<GetDestinationByIdDto> GetDestinationByIdAsync(string id);
}