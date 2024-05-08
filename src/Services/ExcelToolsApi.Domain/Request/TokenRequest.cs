namespace ExcelToolsApi.Domain.Request;

public class TokenRequest
{
    public Guid UserId { get; set; }
    public required string FirstName { get; set; }
}
