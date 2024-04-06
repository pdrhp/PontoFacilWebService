using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PontoFacilSharedData.Models;

namespace PontoFacilSharedData.Data;

public class PontoFacilDbContext : IdentityDbContext<User>
{
    public PontoFacilDbContext(DbContextOptions<PontoFacilDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Person> Person { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<TimeRecord> TimeRecords { get; set; }
}