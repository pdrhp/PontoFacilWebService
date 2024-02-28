using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Models;

namespace PontoFacilSharedData.Interfaces;

public interface IMapperService
{
    User MapUserDtoToUser(CreateUserDto userDto);
    Address MapAddressDtoToAddress(CreateAddressDto addressDto);
    Person MapPersonDtoToPerson(CreatePersonDto personDto, int addressId, string userId);
    List<ReadPersonDto> MapListPersonToListPersonDto(List<Person> persons);

}