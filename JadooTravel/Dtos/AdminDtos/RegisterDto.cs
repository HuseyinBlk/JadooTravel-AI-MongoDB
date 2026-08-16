using System.ComponentModel.DataAnnotations;

namespace JadooTravel.Dtos.AdminDtos;

public class RegisterDto
{
    [Required(ErrorMessage = "Ad zorunludur.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Soyad zorunludur.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta adresi zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [MinLength(3, ErrorMessage = "Şifre en az 3 karakter olmalıdır.")]
    public string Password { get; set; } = string.Empty;
}