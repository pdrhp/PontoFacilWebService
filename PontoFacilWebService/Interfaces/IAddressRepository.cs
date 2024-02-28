using PontoFacilSharedData.Models;

namespace PontoFacilWebService.Interfaces;

public interface IAddressRepository
{
    Task<Address> NewAddress(Address address);
}