using System.ComponentModel.DataAnnotations;

namespace PontoFacilSharedData.Models;

public class Address
{
    [Key]
    [Required]
    public int EnderecoId { get; set; }

    [Required]
    public string Cep { get; set; }

    [Required]
    public string Logradouro { get; set; }
}