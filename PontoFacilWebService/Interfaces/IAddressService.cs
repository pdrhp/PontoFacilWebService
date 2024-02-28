using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Models;

namespace PontoFacilWebService.Interfaces;

public interface IAddressService
{
    Task<Address> CreateAddress(CreateAddressDto addressDto);
}