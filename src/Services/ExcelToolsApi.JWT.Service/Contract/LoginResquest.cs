namespace ExcelToolsApi.JWT.Service.Contract;

public record LoginResquest(
    string Email,
    string Password
);