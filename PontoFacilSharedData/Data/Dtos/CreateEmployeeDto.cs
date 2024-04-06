using System.ComponentModel.DataAnnotations;

namespace PontoFacilSharedData.Data.Dtos;

public class CreateEmployeeDto
{
    [Required]
    public string Cargo { get; set; }
    [Required]
    public float Salario { get; set; }
}