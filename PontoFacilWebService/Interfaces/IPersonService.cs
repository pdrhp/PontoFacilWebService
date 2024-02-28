using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Models;

namespace PontoFacilWebService.Interfaces;

public interface IPersonService
{
    Task<Person> CreatePerson(CreatePersonDto person, int addressId, string userId);
    Task<List<ReadPersonDto>> GetAllPerson();
}