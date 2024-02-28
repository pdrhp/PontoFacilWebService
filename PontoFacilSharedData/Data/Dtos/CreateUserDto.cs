using System.ComponentModel.DataAnnotations;
using PontoFacilWebService.Constants;

namespace PontoFacilSharedData.Data.Dtos;

public class CreateUserDto
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string RePassword { get; set; }
    
    public CreatePersonDto PersonDto { get; set; }
    
    public CreateAddressDto AddressDto { get; set; }

}