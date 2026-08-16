using JadooTravel.Services.PartnerServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents;

public class _DefaultPartnerComponentPartial(IPartnerService _partnerService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _partnerService.GetAllPartnersAsync();
        return View(values);
    }
}