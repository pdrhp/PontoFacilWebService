using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Interfaces;
using PontoFacilSharedData.Models;

namespace PontoFacilSharedData.Mapper;

public class MapperService : IMapperService
{
    public User MapUserDtoToUser(CreateUserDto userDto)
    {
        return new User
        {
            UserName = userDto.Username,
            Email = userDto.Email,
        };
    }

    public Address MapAddressDtoToAddress(CreateAddressDto addressDto)
    {
        return new Address()
        {
            Cep = addressDto.Cep,
            Logradouro = addressDto.Logradouro
        };
    }

    public Person MapPersonDtoToPerson(CreatePersonDto personDto, int addressId, string userId)
    {
        return new Person
        {
            Nome = personDto.Nome,
            DataNascimento = personDto.DataNascimento,
            Gender = personDto.Gender,
            CPF = personDto.CPF,
            AddressId = addressId,
            UserId = userId
        };
    }

    public List<ReadPersonDto> MapListPersonToListPersonDto(List<Person> persons)
    {
        var readPersonDtos = new List<ReadPersonDto>();
        
        foreach (var person in persons)
        {
            var readPersonDto = new ReadPersonDto
            {
                Id = person.Id,
                Nome = person.Nome,
                DataNascimento = person.DataNascimento,
                Gender = person.Gender,
                CPF = person.CPF,
                Endereco = new ReadAddressDto
                {
                    EnderecoId = person.Address.EnderecoId,
                    Cep = person.Address.Cep,
                    Logradouro = person.Address.Logradouro
                },
                User = new ReadUserDto
                {
                    UserId = person.User.Id,
                    Username = person.User.UserName,
                    Email = person.User.Email
                }
            };
            
            readPersonDtos.Add(readPersonDto);
        }

        return readPersonDtos;
    }

    public Employee MapEmployeeDtoToEmployee(CreateEmployeeDto employeeDto, int personId)
    {
        return new Employee
        {
            Cargo = employeeDto.Cargo,
            Salario = employeeDto.Salario,
            PersonId = personId
        };
    }

    public TimeRecord CreateEntryTimeRecord(DateTime entryTime, int employeeId)
    {
        return new TimeRecord
        {
            EmployeeId = employeeId,
            EntryTime = entryTime
        };
    }
}