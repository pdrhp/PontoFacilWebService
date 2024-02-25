using authenticationApi.Data.Dtos;
using authenticationApi.Models;

namespace authenticationApi.Interfaces;

public interface IUserService
{
    Task<RegisterResponse> SignUpUser(CreateUserDto user);
    Task<RegisterResponse> SignUpSuperUser(CreateUserDto user);
    Task<LoginResponse> SignInUser(LoginUserDto user);
}