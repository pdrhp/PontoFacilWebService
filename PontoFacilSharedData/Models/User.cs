using Microsoft.AspNetCore.Identity;

namespace authenticationApi.Models;

public class User : IdentityUser
{
    public DateTime DataNascimento { get; set; }
    public User() : base() { }
}