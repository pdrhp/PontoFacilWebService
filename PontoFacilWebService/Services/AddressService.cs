using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Interfaces;
using PontoFacilSharedData.Models;
using PontoFacilWebService.Interfaces;

namespace PontoFacilWebService.Services;

public class AddressService : IAddressService
{
    private IMapperService _mapper;
    private IAddressRepository _addressRepository;
    
    public AddressService(IMapperService mapper, IAddressRepository addressRepository)
    {
        _mapper = mapper;
        _addressRepository = addressRepository;
    }
    
    public Task<Address> CreateAddress(CreateAddressDto addressDto)
    {
        Address address = _mapper.MapAddressDtoToAddress(addressDto);
        var newAddress = _addressRepository.NewAddress(address);

        return newAddress;
    }
}