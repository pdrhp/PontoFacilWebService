using PontoFacilSharedData.Data.Dtos;

namespace authenticationApi.Interfaces;

public interface IUserService
{
    Task<IGeneralResponse> SignUpUser(CreateUserDto user);
    Task<IGeneralResponse> SignUpSuperUser(CreateUserDto user);
    Task<IGeneralResponse> SignInUser(LoginUserDto user);
    Task<IGeneralResponse> GetRole(string id);
    IGeneralResponse ValidateToken(string token);
}