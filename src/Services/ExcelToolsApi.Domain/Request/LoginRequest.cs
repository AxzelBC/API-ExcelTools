namespace ExcelToolsApi.Domain.Request;

public class LoginResquest
{

    public required string Email { get; set; }
    public required string Password { get; set; }

}