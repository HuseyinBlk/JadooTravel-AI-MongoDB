using JadooTravel.Dtos.AdminDtos;
using JadooTravel.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

[Route("/Register")]
public class RegisterController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
{
    [HttpGet("")]
    [HttpGet("Index")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("")]
    [HttpPost("Index")]
    public async Task<IActionResult> Index(RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return View(registerDto);
        }

        var user = new AppUser
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Dashboard");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(registerDto);
    }
}