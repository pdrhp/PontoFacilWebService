using authenticationApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace authenticationApi.Data;

public class AuthenticationDbContext : IdentityDbContext<User>
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> opts) : base(opts)
    {
        
    }
}