using ExcelToolsApi.Domain.Request;
using ExcelToolsApi.Domain.Response;
using MediatR;

namespace ExcelToolsApi.JWT.Service;
public class AuthenticationTokenRequestAdapter : IRequest<AuthenticationResponse>
{
    public Guid UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
