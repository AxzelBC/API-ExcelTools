using ExcelToolsApi.Domain.DTO;
using ExcelToolsApi.Domain.Request;
using ExcelToolsApi.Domain.Response;
using ExcelToolsApi.JWT.Service.Contract;
using MediatR;

namespace ExcelToolsApi.JWT.Service.Implementation
{
    public class AuthenticationService : IRequestHandler<AuthenticationRegisterAdapter, AuthenticationResponse>, IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        // Método Handle para manejar la solicitud de registro
        public Task<AuthenticationResponse> Handle(AuthenticationRegisterAdapter request, CancellationToken cancellationToken)
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

            // Crear un objeto AuthenticationResponse con la información de respuesta
            var response = new AuthenticationResponse
            {
                Id = userId, // Asigna el mismo userId generado previamente
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Token = token
            };

            // Devuelve el objeto AuthenticationResponse como tarea completada
            return Task.FromResult(response);
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
