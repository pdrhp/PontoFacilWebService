using PontoFacilSharedData.Models;

namespace PontoFacilWebService.Interfaces;

public interface IPersonRepository
{
    Task<Person> NewPerson(Person person);
    Task<List<Person>> GetAllPerson();
}