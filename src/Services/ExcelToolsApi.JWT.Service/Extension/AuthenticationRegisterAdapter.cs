using ExcelToolsApi.Domain.Response;
using MediatR;

namespace ExcelToolsApi.JWT.Service;
public class AuthenticationRegisterAdapter : IRequest<AuthenticationResponse>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

}
