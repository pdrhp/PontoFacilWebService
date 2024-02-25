using authenticationApi.Data.Dtos;
using authenticationApi.Interfaces;
using authenticationApi.Models;
using Microsoft.AspNetCore.Identity;

namespace authenticationApi.Services;

public class UserService : IUserService
{
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private RoleManager<IdentityRole> _roleManager;
    private IMapperService _mapperService;
    private ITokenService _tokenService;
    
    
    public UserService(SignInManager<User> signInManager, UserManager<User> userManager, IMapperService mapperService, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mapperService = mapperService;
        _tokenService = tokenService;
        _roleManager = roleManager;
    }
    
    
    public async Task<RegisterResponse> SignUpUser(CreateUserDto userDto)
    {
        if (userDto.Password != userDto.RePassword)
        {
            throw new BadHttpRequestException("As senhas não conferem!");
        }
        
        User newUser = _mapperService.MapUserDtoToUser(userDto);

        var checkUser = await _userManager.FindByNameAsync(newUser.UserName);
        if (checkUser is not null) return new RegisterResponse(false, "Usuario já cadastrado!", checkUser);
        
        IdentityResult result = await _userManager.CreateAsync(newUser, userDto.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException("Falha ao cadastrar usuário!");
        }
        
        var checkEmployee = await _roleManager.RoleExistsAsync("Employee");
        if(!checkEmployee)
        {
            await _roleManager.CreateAsync(new IdentityRole() {Name = "Employee"});
        }

        await _userManager.AddToRoleAsync(newUser, "Employee");
        
        return new RegisterResponse(true, "Usuário cadastrado com sucesso!", newUser);
    }

    public async Task<RegisterResponse> SignUpSuperUser(CreateUserDto userDto)
    {
        if(userDto.Password != userDto.RePassword)
        {
            throw new BadHttpRequestException("As senhas não conferem!");
        }

        User newUser = _mapperService.MapUserDtoToUser(userDto);

        var checkUser = await _userManager.FindByNameAsync(newUser.UserName);
        if (checkUser is not null) return new RegisterResponse(false, "Usuario já cadastrado!", checkUser);

        IdentityResult result = await _userManager.CreateAsync(newUser, userDto.Password);
        if(!result.Succeeded) throw new ApplicationException("Falha ao cadastrar usuário!");

        var checkAdmin = await _roleManager.RoleExistsAsync("Admin");
        if (!checkAdmin)
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
        }

        await _userManager.AddToRoleAsync(newUser, "Admin");

        return new RegisterResponse(true, "Admnistrador cadastrado com sucesso", newUser);
    }

    public async Task<LoginResponse> SignInUser(LoginUserDto dto)
    {
        var resultado = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);
        
        
        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Usuario não autenticado!");
        }

        var usuario = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

        var getUserRole = await _userManager.GetRolesAsync(usuario);

        var userSession = new UserSession(usuario.Id, usuario.UserName,usuario.DataNascimento.ToString(),getUserRole.FirstOrDefault());
        
        var token = _tokenService.GenerateToken(userSession);

        return new LoginResponse(true, token, "usuario Autenticado com sucesso!");
    }
}