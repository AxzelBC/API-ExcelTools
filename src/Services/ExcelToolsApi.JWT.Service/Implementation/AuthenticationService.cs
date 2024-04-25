using ExcelToolsApi.Domain.DTO;
using ExcelToolsApi.Domain.Request;
using ExcelToolsApi.Domain.Response;
using ExcelToolsApi.JWT.Service.Contract;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExcelToolsApi.JWT.Service.Implementation
{
    public class AuthenticationService : IRequestHandler<AuthenticationRegisterAdapter, AuthenticationResponse>, IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, UserManager<IdentityUser> userManager
        )
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
        }

        // Método Handle para manejar la solicitud de registro
        public async Task<AuthenticationResponse> Handle(AuthenticationRegisterAdapter request, CancellationToken cancellationToken)
        {
            // Verificar si el usuario existe
            // Caso de que exitsa retornar un error
            // Generar un nuevo GUID para el usuario
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

        // Implementación del método Register
        public Task<AuthenticationResponse> Register(AuthenticationRegisterAdapter request)
        {
            // Delegamos el manejo de la solicitud al método Handle definido anteriormente
            return Handle(request, new CancellationToken());
        }

        // Implementación del método Login
        public Task<AuthenticationResponse> Login(LoginRequestDTO loginRequestDTO)
        {
            // EMAIL AND PASWORD
            // Busacar el usuario con el email

            Guid userId = Guid.NewGuid();

            // Crear un objeto TokenRequest con la información del usuario
            TokenRequest tokenRequest = new TokenRequest
            {
                UserId = userId,
                FirstName = "",
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
                Email = loginRequestDTO.Email,
                Token = token
            };

            // Devuelve el objeto AuthenticationResponse como tarea completada
            return Task.FromResult(response);
        }

        // Implementación explícita de la interfaz IAuthenticationService.Login
        Task<AuthenticationResponse> IAuthenticationService.Login(LoginRequestDTO loginRequestDTO)
        {
            // Delegamos a la implementación pública de Login
            return Login(loginRequestDTO);
        }
    }
}
