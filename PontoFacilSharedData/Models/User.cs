using Microsoft.AspNetCore.Identity;

namespace PontoFacilSharedData.Models;

public class User : IdentityUser
{
    public DateTime DataNascimento { get; set; }
    public User() : base() { }
}