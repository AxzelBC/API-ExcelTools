using ExcelToolsApi.JWT.Service.Contract;
using ExcelToolsApi.JWT.Service.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace ExcelTools.API.Controller;

[Route("api/auth")]
[ApiController]
public class AutheticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AutheticationController(IAuthenticationService auth)
    {
        _authenticationService = auth;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterResquest request)
    {
        var authResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
            );
        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token
        );
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginResquest request)
    {
        var authResult = _authenticationService.Login(
            request.Email,
            request.Password
            );
        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token
        );
        return Ok(response);
    }

}
