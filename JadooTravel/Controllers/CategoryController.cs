using JadooTravel.Dtos.CategoryDtos;
using JadooTravel.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

[Authorize]
[Route("/Admin/Category")]
public class CategoryController(ICategoryService categoryService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> CategoryList()
    {
        var values = await categoryService.GetAllCategoriesAsync();
        return View(values);
    }

    [HttpGet("Create")]
    public IActionResult CreateCategory()
    {
        return View();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        await categoryService.CreateCategoryAsync(createCategoryDto);
        return RedirectToAction("CategoryList");
    }

    [HttpGet("Update/{id}")]
    public async Task<IActionResult> UpdateCategory(string id)
    {
        var value = await categoryService.GetCategoryByIdAsync(id);
        return View(value);
    }

    [HttpPost("Update/{id}")]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        await categoryService.UpdateCategoryAsync(updateCategoryDto);
        return RedirectToAction("CategoryList");
    }

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        await categoryService.DeleteCategoryAsync(id);
        return RedirectToAction("CategoryList");
    }
}