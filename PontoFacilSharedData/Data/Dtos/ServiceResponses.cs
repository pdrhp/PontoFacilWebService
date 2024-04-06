using PontoFacilSharedData.Models;

namespace PontoFacilSharedData.Data.Dtos;

public interface IGeneralResponse
{
    bool Flag { get; }
    string Message { get; }
}

public record ErrorResponse(bool Flag, string Message) : IGeneralResponse;

public record ValidateTokenResponse(bool Flag, string Message,bool ValidToken,string token) : IGeneralResponse;


public record RegisterResponse(bool Flag, string Message, User User) : IGeneralResponse;


public record LoginResponse(bool Flag, string Message, string Token, string UserId) : IGeneralResponse;


public record GetAllPersonResponse(bool Flag, string Message, List<ReadPersonDto> Persons) : IGeneralResponse;


public record TimeRecordResponse(bool Flag, string Message, TimeRecord TimeRecord) : IGeneralResponse;


public record GetRoleResponse(bool Flag, string Message, List<string> Roles) : IGeneralResponse;
