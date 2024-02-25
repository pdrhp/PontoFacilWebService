using authenticationApi.Models;

namespace authenticationApi.Data.Dtos;

public record class ErrorResponse(bool Flag, string Message);

public record class RegisterResponse(bool Flag, string Message, User user);

public record class LoginResponse(bool Flag, string Token, string Message);