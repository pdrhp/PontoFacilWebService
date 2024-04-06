using authenticationApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PontoFacilSharedData.Data.Dtos;

namespace authenticationApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private IUserService _userService;
    
    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUpUser([FromBody] CreateUserDto dto)
    {
        IGeneralResponse response = await _userService.SignUpUser(dto);
        return Created("Sucesso",response);
    }

    [HttpPost("signup/admin")]
    public async Task<IActionResult> SignUpAdmin([FromBody] CreateUserDto dto)
    {
        IGeneralResponse response = await _userService.SignUpSuperUser(dto);
        if (!response.Flag)
        {
            return BadRequest(response);
        }
        

        return Created("Sucesso", response);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignInUser([FromBody] LoginUserDto dto)
    {
        var token = await _userService.SignInUser(dto);
        if (token.Flag == false)
        {
            return BadRequest(token);
        }
        return Ok(token);
    }

    [HttpGet("role/{id}")]
    public async Task<IActionResult> GetRole(string id)
    {
        var rolesResponse = await _userService.GetRole(id);
        if (rolesResponse.Flag == false)
        {
            return BadRequest(rolesResponse);
        }

        return Ok(rolesResponse);
    }

    [HttpGet("validatetoken")]
    public IActionResult ValidateToken([FromHeader] string Authorization)
    {
        var token = Authorization.Split(" ")[1];
        
        var response = _userService.ValidateToken(token);

        return Ok(response);
    }
    
}