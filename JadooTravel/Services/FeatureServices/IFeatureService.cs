using JadooTravel.Dtos.FeatureDtos;

namespace JadooTravel.Services.FeatureServices;

public interface IFeatureService
{
    Task<List<ResultFeatureDto>> GetAllFeaturesAsync();
    Task UpdateFeatureAsync(UpdateFeatureDto featureDto);
    Task CreateFeatureAsync(CreateFeatureDto featureDto);
    Task DeleteFeatureAsync(string id);
    Task<GetFeatureByIdDto> GetFeatureByIdAsync(string id);
    
}