using System.ComponentModel.DataAnnotations;

namespace JadooTravel.Dtos.AdminDtos;

public class LoginDto
{
    [Required(ErrorMessage = "E-posta adresi zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}