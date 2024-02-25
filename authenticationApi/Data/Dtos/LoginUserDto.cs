using System.ComponentModel.DataAnnotations;

namespace authenticationApi.Data.Dtos;

public class LoginUserDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}