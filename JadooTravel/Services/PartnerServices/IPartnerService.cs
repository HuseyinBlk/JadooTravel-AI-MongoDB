using JadooTravel.Dtos.PartnerDtos;

namespace JadooTravel.Services.PartnerServices;

public interface IPartnerService
{
    Task<List<ResultPartnerDto>> GetAllPartnersAsync();
    Task UpdatePartnerAsync(UpdatePartnerDto partnerDto);
    Task CreatePartnerAsync(CreatePartnerDto partnerDto);
    Task DeletePartnerAsync(string id);
    Task<GetPartnerByIdDto> GetPartnerByIdAsync(string id);
}