namespace ExcelToolsApi.Domain.Response;

public class AuthenticationResponse
{

    public Guid Id { get; set; }
    public required string Token { get; set; }

}