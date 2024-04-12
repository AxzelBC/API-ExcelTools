using ExcelToolsApi.Domain.DTO;
using ExcelToolsApi.Domain.Request;
using ExcelToolsApi.Domain.Response;
using ExcelToolsApi.JWT.Service.Commands.CreateUser;

namespace ExcelToolsApi.JWT.Service.Contract;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> Register(AuthenticationRegisterAdapter request);
    Task<AuthenticationResponse> Login(LoginRequestDTO loginRequestDTO);
}