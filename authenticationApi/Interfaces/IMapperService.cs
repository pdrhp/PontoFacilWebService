using authenticationApi.Data.Dtos;
using authenticationApi.Models;

namespace authenticationApi.Interfaces;

public interface IMapperService
{
    User MapUserDtoToUser(CreateUserDto userDto);
    
}