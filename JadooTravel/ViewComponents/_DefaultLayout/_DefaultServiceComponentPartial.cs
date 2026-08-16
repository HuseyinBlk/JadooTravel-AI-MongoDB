using JadooTravel.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents;

public class _DefaultServiceComponentPartial(ICategoryService _categoryService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _categoryService.GetAllCategoriesAsync();
        
        var activeCategories = values.Where(x => x.Status).Take(4).ToList();
        
        return View(activeCategories);
    }
}