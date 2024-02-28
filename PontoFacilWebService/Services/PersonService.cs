using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Interfaces;
using PontoFacilSharedData.Models;
using PontoFacilWebService.Interfaces;

namespace PontoFacilWebService.Services;

public class PersonService : IPersonService
{
    private IMapperService _mapper;
    private IPersonRepository _personRepository;
    
    public PersonService(IMapperService mapper, IPersonRepository personRepository)
    {
        _mapper = mapper;
        _personRepository = personRepository;
    }
    
    
    public Task<Person> CreatePerson(CreatePersonDto personDto, int addressId, string userId)
    {
        var mappedPerson = _mapper.MapPersonDtoToPerson(personDto, addressId, userId);
        return  _personRepository.NewPerson(mappedPerson);
    }

    public async Task<List<ReadPersonDto>> GetAllPerson()
    {
        var persons = await _personRepository.GetAllPerson();

        var mappedPersons = _mapper.MapListPersonToListPersonDto(persons);
        
        return mappedPersons;
    }
}