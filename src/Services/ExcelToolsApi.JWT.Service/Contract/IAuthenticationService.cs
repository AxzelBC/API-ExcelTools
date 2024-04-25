using ExcelToolsApi.Domain;
using ExcelToolsApi.Domain.DTO;
using ExcelToolsApi.Domain.Response;

namespace ExcelToolsApi.JWT.Service.Contract;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> Register(AuthenticationRegisterAdapter request);
    Task<AuthenticationResponse> Login(LoginRequestDTO loginRequestDTO);
    Task<AuthenticationResponse> RenovateToken(TokenRequestDTO tokenRequestDTO);
}