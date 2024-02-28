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
        RegisterResponse response = await _userService.SignUpUser(dto);
        return Created("Sucesso",response);
    }

    [HttpPost("signup/admin")]
    public async Task<IActionResult> SignUpAdmin([FromBody] CreateUserDto dto)
    {
        RegisterResponse response = await _userService.SignUpSuperUser(dto);
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
        return Ok(token);
    }
    
}