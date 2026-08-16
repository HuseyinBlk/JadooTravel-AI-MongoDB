using JadooTravel.Services.FeatureServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Dtos.FeatureDtos;

[Authorize]
[Route("/Admin/Feature")]
public class FeatureController(IFeatureService featureService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Feature()
    {
        var values = await featureService.GetAllFeaturesAsync();
        return View(values);
    }
    
    [HttpGet("Create")]
    public async Task<IActionResult> CreateFeature()
    {
        return View();
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
    {
        await featureService.CreateFeatureAsync(createFeatureDto);
        return RedirectToAction("Feature");
    }

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> DeleteFeature(string id)
    {
        await featureService.DeleteFeatureAsync(id);
        return RedirectToAction("Feature");
    }

    [HttpGet("Update/{id}")]
    public async Task<IActionResult> UpdateFeature(string id)
    {
        var value = await featureService.GetFeatureByIdAsync(id);
        return View(value);
    }

    [HttpPost("Update/{id}")]
    public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
    {
        await featureService.UpdateFeatureAsync(updateFeatureDto);
        return RedirectToAction("Feature");
    }
}