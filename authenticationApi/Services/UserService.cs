using authenticationApi.Interfaces;
using Microsoft.AspNetCore.Identity;
using PontoFacilSharedData.Data;
using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Interfaces;
using PontoFacilSharedData.Models;
using PontoFacilWebService.Interfaces;

namespace authenticationApi.Services;

public class UserService : IUserService
{
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private RoleManager<IdentityRole> _roleManager;
    private IMapperService _mapperService;
    private ITokenService _tokenService;
    private IAddressService _addressService;
    private IPersonService _personService;
    
    
    public UserService(SignInManager<User> signInManager, UserManager<User> userManager, IMapperService mapperService, ITokenService tokenService, RoleManager<IdentityRole> roleManager, IPersonService personService, IAddressService addressService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mapperService = mapperService;
        _tokenService = tokenService;
        _roleManager = roleManager;
        _personService = personService;
        _addressService = addressService;
    }
    
    
    public async Task<RegisterResponse> SignUpUser(CreateUserDto userDto)
    {
        if (userDto is null)
        {
            throw new ArgumentException("usuario não pode ser nulo");
        }
        
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
        
        var createdUser = await _userManager.FindByEmailAsync(userDto.Email);
        
        var createdAddress = await _addressService.CreateAddress(userDto.AddressDto);
        
        if(createdUser is not null && createdAddress is not null)
        {
            var createdPerson =
                await _personService.CreatePerson(userDto.PersonDto, createdAddress.EnderecoId, createdUser.Id);
        }
        
        return new RegisterResponse(true, "Usuário cadastrado com sucesso!", createdUser);
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

        var userSession = new UserSession(usuario.Id, usuario.UserName,getUserRole.FirstOrDefault());
        
        var token = _tokenService.GenerateToken(userSession);

        return new LoginResponse(true, token, "usuario Autenticado com sucesso!");
    }
}