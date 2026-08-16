using JadooTravel.Dtos.TestimonialDtos;
using JadooTravel.Services.TestimonialServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

[Authorize]
[Route("/Admin/Testimonial")]
public class TestimonialController(ITestimonialService _testimonialService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> TestimonialList()
    {
        var values = await _testimonialService.GetAllTestimonialsAsync();
        return View(values);
    }
    
    [HttpGet("Create")]
    public async Task<IActionResult> CreateTestimonial()
    {
        return View();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto testimonialDto)
    {
        await _testimonialService.CreateTestimonialAsync(testimonialDto);
        return RedirectToAction("TestimonialList");
    }
    
    [HttpGet("Update/{id}")]
    public async Task<IActionResult> UpdateTestimonial(string id)
    {
        var value = await _testimonialService.GetTestimonialByIdAsync(id);
        return View(value);
    }

    [HttpPost("Update/{id}")]
    public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto testimonialDto)
    {
        await _testimonialService.UpdateTestimonialAsync(testimonialDto);
        return RedirectToAction("TestimonialList");
    }
    
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> DeleteTestimonial(string id)
    {
        await _testimonialService.DeleteTestimonialAsync(id);
        return RedirectToAction("TestimonialList");
    }
}