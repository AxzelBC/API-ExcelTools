using ExcelToolsApi.Domain.Response;
using ExcelToolsApi.JWT.Service.Contract;
using MediatR;

namespace ExcelToolsApi.JWT.Service.Commands.CreateUser;


// TRequest,TResponse
public class CreateUserCommandHandler : IRequestHandler<AuthenticationRegisterAdapter, AuthenticationResponse>
{
    #region private fields
    private readonly IAuthenticationService _authenticationService;

    #endregion private fields
    public CreateUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<AuthenticationResponse> Handle(AuthenticationRegisterAdapter request, CancellationToken cancellationToken)
    {
        var response = await _authenticationService.Register(request);
        return response;
    }
}
