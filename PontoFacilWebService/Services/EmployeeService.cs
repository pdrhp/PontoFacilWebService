using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Interfaces;
using PontoFacilSharedData.Models;
using PontoFacilWebService.Interfaces;

namespace PontoFacilWebService.Services;

public class EmployeeService : IEmployeeService
{
    private IEmployeeRepository _employeeRepository;
    private IMapperService _mapperService;
    public EmployeeService(IEmployeeRepository employeeRepository, IMapperService mapperService)
    {
        _employeeRepository = employeeRepository;
        _mapperService = mapperService;
    }
    
    public Task<Employee> CreateEmployee(CreateEmployeeDto employeeDto, int personId)
    { 
        var mappedEmployee = _mapperService.MapEmployeeDtoToEmployee(employeeDto, personId);
        return _employeeRepository.NewEmployee(mappedEmployee);
    }
}