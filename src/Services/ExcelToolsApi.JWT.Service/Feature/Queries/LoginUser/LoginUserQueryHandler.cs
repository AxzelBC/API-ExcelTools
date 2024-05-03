using ExcelToolsApi.Domain.Request;
using ExcelToolsApi.Domain.Response;
using ExcelToolsApi.JWT.Service.Contract;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExcelToolsApi.JWT.Service.Commands.GetUser;


// TRequest,TResponse
public class LoginUserCommandHandler : IRequestHandler<AuthenticationLoginAdapter, AuthenticationResponse>
{

    #region private fields
    private readonly IAuthenticationService _authenticationService;

    #endregion private fields
    public LoginUserCommandHandler(
        IAuthenticationService authenticationService
    )
    {
        _authenticationService = authenticationService;
    }

    public async Task<AuthenticationResponse> Handle(AuthenticationLoginAdapter loginRequestDTO, CancellationToken cancellationToken)
    {
        var response = await _authenticationService.Login(loginRequestDTO);
        return response;
    }
}
