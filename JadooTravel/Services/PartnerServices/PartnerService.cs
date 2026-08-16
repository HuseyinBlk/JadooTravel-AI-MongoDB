using AutoMapper;
using JadooTravel.Dtos.PartnerDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.PartnerServices;

public class PartnerService(IMongoDatabase database, IMapper _mapper, IDatabaseSettings databaseSettings): IPartnerService
{
    private readonly IMongoCollection<Partner> _partnerCollection = database.GetCollection<Partner>(databaseSettings.PartnerCollectionName);
    
    public async Task<List<ResultPartnerDto>> GetAllPartnersAsync()
    {
        var values = await _partnerCollection.Find(x=>true).ToListAsync();
        return _mapper.Map<List<ResultPartnerDto>>(values);
    }

    public async Task UpdatePartnerAsync(UpdatePartnerDto partnerDto)
    {
        var value = _mapper.Map<Partner>(partnerDto);
        await _partnerCollection.FindOneAndReplaceAsync(x => x.PartnerId == partnerDto.PartnerId, value);
    }

    public async Task CreatePartnerAsync(CreatePartnerDto partnerDto)
    {
        var value = _mapper.Map<Partner>(partnerDto);
        await _partnerCollection.InsertOneAsync(value);
    }

    public async Task DeletePartnerAsync(string id)
    {
        await _partnerCollection.DeleteOneAsync(x => x.PartnerId == id);
    }

    public async Task<GetPartnerByIdDto> GetPartnerByIdAsync(string id)
    {
        var value = await _partnerCollection.Find(x => x.PartnerId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetPartnerByIdDto>(value);
    }
}