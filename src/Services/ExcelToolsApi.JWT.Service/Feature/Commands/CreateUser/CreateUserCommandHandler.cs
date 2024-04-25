﻿using ExcelToolsApi.Domain.Request;
using ExcelToolsApi.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExcelToolsApi.JWT.Service.Commands.CreateUser;


// TRequest,TResponse
public class CreateUserCommandHandler : IRequestHandler<AuthenticationRegisterAdapter, AuthenticationResponse>
{
    #region private fields
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly UserManager<IdentityUser> _userManager;

    #endregion private fields
    public CreateUserCommandHandler(UserManager<IdentityUser> userManager
, IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
    }

    public async Task<AuthenticationResponse> Handle(AuthenticationRegisterAdapter request, CancellationToken cancellationToken)
    {
        Guid userId = Guid.NewGuid();

        // Crear un objeto TokenRequest con la información del usuario
        TokenRequest tokenRequest = new TokenRequest
        {
            UserId = userId,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        // Generar el token utilizando el generador de tokens
        var token = _jwtTokenGenerator.GenerateToken(tokenRequest);

        var user = new IdentityUser { UserName = request.Email, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);

        // Crear un objeto AuthenticationResponse con la información de respuesta
        var response = new AuthenticationResponse
        {
            Id = userId, // Asigna el mismo userId generado previamente
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Token = token
        };
        if (result.Succeeded)
        {
            // Devuelve el objeto AuthenticationResponse como tarea completada
            return response;

        }
        return response;
    }
}
