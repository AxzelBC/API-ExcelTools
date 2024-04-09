namespace ExcelToolsApi.JWT.Service.Contract;

public record RegisterResquest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);