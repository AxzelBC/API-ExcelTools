using ExcelToolsApi.Domain.Request;
using ExcelToolsApi.Domain.Response;
using MediatR;

namespace ExcelToolsApi.JWT.Service;
public class AuthenticationTokenRequestAdapter : IRequest<AuthenticationResponse>
{
    public required string Id { get; set; }
}
