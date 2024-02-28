using System.ComponentModel.DataAnnotations;
using PontoFacilWebService.Constants;

namespace PontoFacilSharedData.Data.Dtos;

public class CreatePersonDto
{
    [Required]
    public string Nome { get; set; }
    [Required]
    public DateTime DataNascimento { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    public string CPF { get; set; }
}