using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authenticationApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TesteAdminController : ControllerBase
{
    
    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAdmin()
    {
        return Ok("TesteAdmin");
    }

    
    [HttpGet("user")]
    [Authorize]
    public IActionResult GetUser()
    {
        return Ok("TesteUser");
    }

    [HttpGet("anonimo")]
    public IActionResult GetAnonimo()
    {
        return Ok("TesteAnonimo");
    }
    
}