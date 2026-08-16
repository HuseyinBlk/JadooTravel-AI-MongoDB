using JadooTravel.Dtos.AdminDtos;
using JadooTravel.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers;

[Route("/Login")]
public class LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
{
    [HttpGet("Index")]
    [HttpGet("")]
    public IActionResult Index()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }

    [HttpPost("Index")]
    [HttpPost("")]
    public async Task<IActionResult> Index(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return View(loginDto);
        }

        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "E-posta adresi veya şifre hatalı.");
            return View(loginDto);
        }

        var result = await signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe, false);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        ModelState.AddModelError(string.Empty, "E-posta adresi veya şifre hatalı.");
        return View(loginDto);
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Login");
    }
}