using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using authenticationApi.Interfaces;
using Microsoft.IdentityModel.Tokens;
using PontoFacilSharedData.Data.Dtos;

namespace authenticationApi.Services;

public class TokenService : ITokenService
{
    private IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"])),
            ValidateAudience = false,
            ValidateIssuer = false,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = true
        };
    }

    public string GenerateToken(UserSession userSession)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("username", userSession.Name),
            new Claim("id", userSession.Id),
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

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}