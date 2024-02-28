using System.ComponentModel.DataAnnotations;

namespace PontoFacilSharedData.Data.Dtos;

public class CreateAddressDto
{
    [Required]
    public string Cep { get; set; }
    [Required]
    public string Logradouro { get; set; }
}