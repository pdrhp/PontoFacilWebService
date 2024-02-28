using PontoFacilSharedData.Data.Dtos;

namespace authenticationApi.Interfaces;

public interface ITokenService
{
    string GenerateToken(UserSession userSession);
}