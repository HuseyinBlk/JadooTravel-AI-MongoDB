using JadooTravel.Dtos.TripPlanDtos;
using JadooTravel.Services.TripPlanServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

[Authorize]
[Route("/Admin/TripPlan")]
public class TripPlanController(ITripPlanService _tripPlanService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> TripPlanList()
    {
        var values = await _tripPlanService.GetAllTripPlansAsync();
        return View(values);
    }
    [HttpGet("Create")]
    public async Task<IActionResult> CreateTripPlan()
    {
        return View();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateTripPlan(CreateTripPlanDto tripPlanDto)
    {
        await _tripPlanService.CreateTripPlanAsync(tripPlanDto);
        return RedirectToAction("TripPlanList");
    }
    [HttpGet("Update/{id}")]
    public async Task<IActionResult> UpdateTripPlan(string id)
    {
        var value = await _tripPlanService.GetTripPlanByIdAsync(id);
        return View(value);
    }

    [HttpPost("Update/{id}")]
    public async Task<IActionResult> UpdateTripPlan(UpdateTripPlanDto tripPlanDto)
    {
        await _tripPlanService.UpdateTripPlanAsync(tripPlanDto);
        return RedirectToAction("TripPlanList");
    }
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> DeleteTripPlan(string id)
    {
        await _tripPlanService.DeleteTripPlanAsync(id);
        return RedirectToAction("TripPlanList");
    }
}