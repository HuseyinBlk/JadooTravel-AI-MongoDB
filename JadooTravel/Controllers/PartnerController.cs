using JadooTravel.Dtos.PartnerDtos;
using JadooTravel.Services.PartnerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

[Authorize]
[Route("/Admin/Partner")]
public class PartnerController(IPartnerService _partnerService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> PartnerList()
    {   
        var values = await _partnerService.GetAllPartnersAsync();
        return View(values);
    }
    
    [HttpGet("Create")]
    public async Task<IActionResult> CreatePartner()
    {
        return View();
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> CreatePartner(CreatePartnerDto partnerDto)
    {
        await _partnerService.CreatePartnerAsync(partnerDto);
        return RedirectToAction("PartnerList");
    }
    
    [HttpGet("Update/{id}")]
    public async Task<IActionResult> UpdatePartner(string id)
    {
        var value = await _partnerService.GetPartnerByIdAsync(id);
        return View(value);
    }
    
    [HttpPost("Update/{id}")]
    public async Task<IActionResult> UpdatePartner(UpdatePartnerDto partnerDto)
    {
        await _partnerService.UpdatePartnerAsync(partnerDto);
        return RedirectToAction("PartnerList");
    }
    
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> DeletePartner(string id)
    {
        await _partnerService.DeletePartnerAsync(id);
        return RedirectToAction("PartnerList");
    }
}