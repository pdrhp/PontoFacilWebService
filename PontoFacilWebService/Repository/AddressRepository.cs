using PontoFacilSharedData.Data;
using PontoFacilSharedData.Models;
using PontoFacilWebService.Interfaces;

namespace PontoFacilWebService.Repository;

public class AddressRepository : IAddressRepository
{
    private PontoFacilDbContext _context;
    
    public AddressRepository(PontoFacilDbContext context)
    {
        _context = context;
    }
    
    public async Task<Address> NewAddress(Address address)
    {
        var newAddress = _context.Address.Add(address);
        await _context.SaveChangesAsync();
        return newAddress.Entity;
    }
}