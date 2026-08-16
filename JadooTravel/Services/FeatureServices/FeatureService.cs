using AutoMapper;
using JadooTravel.Dtos.FeatureDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.FeatureServices;

public class FeatureService(IMongoDatabase database, IMapper _mapper, IDatabaseSettings databaseSettings) : IFeatureService
{
    private readonly IMongoCollection<Feature> _featureCollection = database.GetCollection<Feature>(databaseSettings.FeatureCollectionName);
    
    public async Task<List<ResultFeatureDto>> GetAllFeaturesAsync()
    {
        var values = await _featureCollection.Find(f => true).ToListAsync();
        return _mapper.Map<List<ResultFeatureDto>>(values);
    }

    public async Task UpdateFeatureAsync(UpdateFeatureDto featureDto)
    {
        var value = _mapper.Map<Feature>(featureDto);
        await _featureCollection.FindOneAndReplaceAsync(x => x.FeatureId == featureDto.FeatureId, value);
    }

    public async Task CreateFeatureAsync(CreateFeatureDto featureDto)
    {
        var value = _mapper.Map<Feature>(featureDto);
        await _featureCollection.InsertOneAsync(value);
    }

    public async Task DeleteFeatureAsync(string id)
    {
        await _featureCollection.DeleteOneAsync(x => x.FeatureId == id);
    }

    public async Task<GetFeatureByIdDto> GetFeatureByIdAsync(string id)
    {
        var value = await _featureCollection.Find(x => x.FeatureId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetFeatureByIdDto>(value);
    }
}