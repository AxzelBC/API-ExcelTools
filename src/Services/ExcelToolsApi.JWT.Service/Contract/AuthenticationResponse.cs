namespace ExcelToolsApi.JWT.Service.Contract;

public record AuthenticationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token
);