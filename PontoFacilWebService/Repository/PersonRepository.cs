using Microsoft.EntityFrameworkCore;
using PontoFacilSharedData.Data;
using PontoFacilSharedData.Models;
using PontoFacilWebService.Interfaces;

namespace PontoFacilWebService.Repository;

public class PersonRepository : IPersonRepository
{
    private PontoFacilDbContext _context;
    
    public PersonRepository(PontoFacilDbContext context)
    {
        _context = context;
    }
    
    public async Task<Person> NewPerson(Person person)
    {
        var newPerson = _context.Person.Add(person);
        await _context.SaveChangesAsync();
        return newPerson.Entity;
    }
    
    public async Task<List<Person>> GetAllPerson()
    {
        return await _context.Person
            .Include(p => p.Address)
            .Include(p => p.User)
            .ToListAsync();
    }
}