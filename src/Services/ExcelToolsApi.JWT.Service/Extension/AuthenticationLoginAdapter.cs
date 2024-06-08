using ExcelToolsApi.Domain.Request;
using ExcelToolsApi.Domain.Response;
using MediatR;

namespace ExcelToolsApi.JWT.Service;
public class AuthenticationLoginAdapter : IRequest<AuthenticationResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
