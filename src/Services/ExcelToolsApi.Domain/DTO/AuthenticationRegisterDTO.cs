namespace ExcelToolsApi.Domain.DTO;

public class AuthenticationResgisterDTO
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Token { get; set; }

}
