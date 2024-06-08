using ExcelToolsApi.Domain.Response;
using ExcelToolsApi.JWT.Service.Contract;
using MediatR;

namespace ExcelToolsApi.JWT.Service.Commands.RenovateToken;


// TRequest,TResponse
public class RenovateTokenQueryHandler : IRequestHandler<AuthenticationTokenRequestAdapter, AuthenticationResponse>
{

    #region private fields
    private readonly IAuthenticationService _authenticationService;
    #endregion private fields
    public RenovateTokenQueryHandler(
        IAuthenticationService authenticationService
    )
    {
        _authenticationService = authenticationService;
    }


    public async Task<AuthenticationResponse> Handle(AuthenticationTokenRequestAdapter request, CancellationToken cancellationToken)
    {
        var response = await _authenticationService.RenovateToken(request);
        return response;
    }
}
