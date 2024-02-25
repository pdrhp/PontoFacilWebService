using authenticationApi.Data.Dtos;
using authenticationApi.Interfaces;
using PontoFacilSharedData.Models;

namespace authenticationApi.Services;

public class MapperService : IMapperService
{
    public User MapUserDtoToUser(CreateUserDto userDto)
    {
        return new User
        {
            UserName = userDto.Username,
            DataNascimento = userDto.DataNascimento,
        };
    }
}