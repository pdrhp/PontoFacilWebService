using System.Text;
using authenticationApi.Interfaces;
using authenticationApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PontoFacilSharedData.Data;
using PontoFacilSharedData.Interfaces;
using PontoFacilSharedData.Mapper;
using PontoFacilSharedData.Models;
using PontoFacilWebService.Interfaces;
using PontoFacilWebService.Repository;
using PontoFacilWebService.Services;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var enviroment = builder.Environment;

if (enviroment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
else
{
    var vaultUri = builder.Configuration["Vault:Uri"];
    var vaultToken = builder.Configuration["Vault:Token"];

    var vaultClientSettings = new VaultClientSettings(vaultUri, new TokenAuthMethodInfo(vaultToken));
    var vaultClient = new VaultClient(vaultClientSettings);

    var secrets = vaultClient.V1.Secrets.KeyValue.V1.ReadSecretAsync(
        path: "secrets",
        mountPoint: "pontofacil"
    ).Result.Data;

    foreach (var secret in secrets)
    {
        builder.Configuration[secret.Key] = secret.Value.ToString();
    }
}


builder.Services.AddDbContext<PontoFacilDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:AuthenticationConnection"], b => b.MigrationsAssembly("PontoFacilWebService"));
});

builder.Services.AddScoped<IMapperService, MapperService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITimeRecordRepository, TimeRecordRepository>();
builder.Services.AddScoped<ITimeRecordService, TimeRecordService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("pontoFacil", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000");
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowCredentials();
    });
});

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<PontoFacilDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("pontoFacil");

app.Run();

// docker run --cap-add SYS_PTRACE -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=rooTDb123" -p 1200:1433 --name ponto-db-authentication -d mcr.microsoft.com/azure-sql-edge