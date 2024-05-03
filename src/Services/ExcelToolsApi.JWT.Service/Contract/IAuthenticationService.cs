using ExcelToolsApi.Domain.Response;

namespace ExcelToolsApi.JWT.Service.Contract;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> Register(AuthenticationRegisterAdapter request);
    Task<AuthenticationResponse> Login(AuthenticationLoginAdapter request);
    Task<AuthenticationResponse> RenovateToken(AuthenticationTokenRequestAdapter request);
}