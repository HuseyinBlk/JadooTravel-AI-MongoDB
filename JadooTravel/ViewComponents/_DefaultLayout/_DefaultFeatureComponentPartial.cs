using JadooTravel.Services.FeatureServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents;

public class _DefaultFeatureComponentPartial(IFeatureService _featureService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _featureService.GetAllFeaturesAsync();
        var feature = values.FirstOrDefault();
        return View(feature);
    }
}