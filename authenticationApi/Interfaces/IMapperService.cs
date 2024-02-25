using authenticationApi.Data.Dtos;
using PontoFacilSharedData.Models;

namespace authenticationApi.Interfaces;

public interface IMapperService
{
    User MapUserDtoToUser(CreateUserDto userDto);
    
}