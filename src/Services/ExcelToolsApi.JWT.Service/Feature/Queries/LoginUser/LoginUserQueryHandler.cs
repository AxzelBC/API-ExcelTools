using ExcelToolsApi.Domain.Request;
using ExcelToolsApi.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExcelToolsApi.JWT.Service.Commands.GetUser;


// TRequest,TResponse
public class LoginUserCommandHandler : IRequestHandler<AuthenticationLoginAdapter, AuthenticationResponse>
{

    #region private fields
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly UserManager<IdentityUser> _userManager;

    #endregion private fields
    public LoginUserCommandHandler(UserManager<IdentityUser> userManager
, IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
    }

    public async Task<AuthenticationResponse> Handle(AuthenticationLoginAdapter loginRequestDTO, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(loginRequestDTO.Email);

        if (user is null)
        {
            throw new ArgumentException();
        }
        if (!await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password))
        {
            throw new ArgumentException($"Unable to authenticate user {loginRequestDTO.Email}");
        }
        var userId = new Guid(user.Id);

        // Crear un objeto TokenRequest con la información del usuario
        TokenRequest tokenRequest = new TokenRequest
        {
            UserId = userId,
            FirstName = user.UserName,
            LastName = ""
        };
        // Generar el token utilizando el generador de tokens
        var token = _jwtTokenGenerator.GenerateToken(tokenRequest);

        // Crear un objeto AuthenticationResponse con la información de respuesta
        var response = new AuthenticationResponse
        {
            Id = userId, // Asigna el mismo userId generado previamente
            FirstName = "",
            LastName = "",
            Email = "",
            Token = token
        };

        return response;
    }
}
