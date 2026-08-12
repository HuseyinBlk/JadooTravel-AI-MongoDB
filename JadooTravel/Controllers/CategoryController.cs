using JadooTravel.Dtos.CategoryDtos;
using JadooTravel.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

public class CategoryController(ICategoryService _categoryService) : Controller
{
    // GET
    public async Task<IActionResult> CategoryList()
    {
        var values = await _categoryService.GetAllCategoriesAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        await _categoryService.CreateCategoryAsync(createCategoryDto);
        return RedirectToAction("CategoryList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateCategory(string id)
    {
        var value = await _categoryService.GetCategoryByIdAsync(id);
        return View(value);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        await _categoryService.UpdateCategoryAsync(updateCategoryDto);
        return RedirectToAction("CategoryList");
    }

    public async Task<IActionResult> DeleteCategory(string id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return RedirectToAction("CategoryList");
    }
}