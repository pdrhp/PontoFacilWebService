using authenticationApi.Data.Dtos;
using authenticationApi.Models;

namespace authenticationApi.Interfaces;

public interface ITokenService
{
    string GenerateToken(UserSession userSession);
}