using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Models;

namespace PontoFacilWebService.Interfaces;

public interface IEmployeeService
{
    Task<Employee> CreateEmployee(CreateEmployeeDto employeeDto, int personId);
}