using PontoFacilSharedData.Data;
using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Models;
using PontoFacilWebService.Interfaces;

namespace PontoFacilWebService.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private PontoFacilDbContext _context;
    
    public EmployeeRepository(PontoFacilDbContext context)
    {
        _context = context;
    }
    
    public async Task<Employee> NewEmployee(Employee employee)
    {
        var newEmployee = _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return newEmployee.Entity;
    }
}