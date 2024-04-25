using ExcelToolsApi.Domain.Request;
using ExcelToolsApi.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExcelToolsApi.JWT.Service.Commands.RenovateToken;


// TRequest,TResponse
public class RenovateTokenQueryHandler : IRequestHandler<AuthenticationTokenRequestAdapter, AuthenticationResponse>
{

    #region private fields
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly UserManager<IdentityUser> _userManager;

    #endregion private fields
    public RenovateTokenQueryHandler(UserManager<IdentityUser> userManager
, IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
    }


    public async Task<AuthenticationResponse> Handle(AuthenticationTokenRequestAdapter request, CancellationToken cancellationToken)
    {
        var userIdString = request.UserId.ToString();
        var user = await _userManager.FindByIdAsync(userIdString);

        if (user is null)
        {
            throw new ArgumentException("User not found");
        }
        var userId = new Guid(user.Id);

        TokenRequest tokenRequest = new TokenRequest
        {
            UserId = userId,
            FirstName = user.UserName,
            LastName = user.UserName
        };
        var token = _jwtTokenGenerator.GenerateToken(tokenRequest);

        var response = new AuthenticationResponse
        {
            Id = userId, // Asigna el mismo userId generado previamente
            FirstName = user.UserName,
            LastName = user.UserName,
            Email = user.Email,
            Token = token
        };

        // Devuelve el objeto AuthenticationResponse como tarea completada
        return response;

    }
}
