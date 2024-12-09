using System.ComponentModel.DataAnnotations;

namespace Talabat.Core.Dtos.auth;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}