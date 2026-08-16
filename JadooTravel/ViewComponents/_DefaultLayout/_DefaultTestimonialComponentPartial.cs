using JadooTravel.Services.TestimonialServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents;

public class _DefaultTestimonialComponentPartial(ITestimonialService _testimonialService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _testimonialService.GetAllTestimonialsAsync();
        return View(values);
    }
}