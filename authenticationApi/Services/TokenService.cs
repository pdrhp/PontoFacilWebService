using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using authenticationApi.Data.Dtos;
using authenticationApi.Interfaces;
using authenticationApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace authenticationApi.Services;

public class TokenService : ITokenService
{
    private IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(UserSession userSession)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("username", userSession.Name),
            new Claim("id", userSession.Id),
            new Claim("dateOfBirth", userSession.DataNascimento),
            new Claim(ClaimTypes.Role, userSession.Role),
            new Claim("loginTimeStamp", DateTime.UtcNow.ToString())
        };

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));

        var signInCrendentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (
            expires: DateTime.Now.AddHours(5),
            claims: claims,
            signingCredentials: signInCrendentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}