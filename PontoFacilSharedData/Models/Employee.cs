using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoFacilSharedData.Models;

public class Employee
{
    [Key]
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public string Cargo { get; set; }
    [Required]
    public float Salario { get; set; }
    [ForeignKey("Person")]
    public int PersonId { get; set; }
    public virtual Person Person { get; set; }
}