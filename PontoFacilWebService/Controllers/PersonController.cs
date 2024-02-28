using Microsoft.AspNetCore.Mvc;
using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Models;
using PontoFacilWebService.Interfaces;

namespace PontoFacilWebService.Controllers;


[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private IPersonService _personService;
    
    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }
    
    [HttpGet]
    public async Task<GetAllPersonResponse> GetAllPersons()
    {
        var persons = await _personService.GetAllPerson();

        return new GetAllPersonResponse(true, "Pessoas encontradas com sucesso", persons);
    }
}