using PontoFacilSharedData.Models;

namespace PontoFacilSharedData.Data.Dtos;

public record class ErrorResponse(bool Flag, string Message);

public record class RegisterResponse(bool Flag, string Message, User user);

public record class LoginResponse(bool Flag, string Token, string Message);

public record class GetAllPersonResponse(bool Flag, string Message, List<ReadPersonDto> Persons);