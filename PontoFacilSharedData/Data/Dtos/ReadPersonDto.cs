using PontoFacilWebService.Constants;

namespace PontoFacilSharedData.Data.Dtos;

public class ReadPersonDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ReadAddressDto Endereco { get; set; }
    public DateTime DataNascimento { get; set; }
    public Gender Gender { get; set; }
    public string CPF { get; set; }
    public ReadUserDto User { get; set; }
}