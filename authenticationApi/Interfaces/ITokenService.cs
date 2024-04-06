using Microsoft.IdentityModel.Tokens;
using PontoFacilSharedData.Data.Dtos;

namespace authenticationApi.Interfaces;

public interface ITokenService
{
    string GenerateToken(UserSession userSession);

    bool ValidateToken(string token);

}