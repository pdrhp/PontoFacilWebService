using PontoFacilSharedData.Data.Dtos;

namespace authenticationApi.Interfaces;

public interface IUserService
{
    Task<RegisterResponse> SignUpUser(CreateUserDto user);
    Task<RegisterResponse> SignUpSuperUser(CreateUserDto user);
    Task<LoginResponse> SignInUser(LoginUserDto user);
}