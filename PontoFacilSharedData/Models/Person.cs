using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using authenticationApi.Models;
using PontoFacilWebService.Constants;

namespace PontoFacilWebService.Models;

public class Person
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [Length(10, 50, ErrorMessage = "O campo nome deve ter entre 10 e 50 caracteres")]
    public string Nome { get; set; }
    
    [ForeignKey("Address")]
    [Required]
    public int AddressId { get; set; }
    public virtual Address Address { get; set; }

    [Required]
    public DateTime DataNascimento { get; set; }

    [Required]
    public Gender Gender { get; set; }
    
    [Required]
    public string CPF { get; set; }
    
    [ForeignKey("User")]
    public string UserId { get; set; }
    public virtual User User { get; set; }
}