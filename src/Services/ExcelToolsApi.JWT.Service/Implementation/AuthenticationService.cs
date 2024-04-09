using System;
using System.Threading;
using System.Threading.Tasks;
using ExcelToolsApi.Domain.DTO;
using ExcelToolsApi.Domain.Response;
using ExcelToolsApi.JWT.Service.Contract;
using MediatR;

namespace ExcelToolsApi.JWT.Service.Implementation
{
    public class AuthenticationService : IRequestHandler<AuthenticationRegisterAdapter, AuthenticationResponse>, IAuthenticationService
    {


        public Task<AuthenticationResponse> Handle(AuthenticationRegisterAdapter request, CancellationToken cancellationToken)
        {
            // Lógica de registro aquí...
            // Aquí se supone que se realiza el registro del usuario en la base de datos o
            // en algún otro sistema de autenticación.

            var response = new AuthenticationResponse
            {
                Id = Guid.NewGuid(), // Genera un nuevo GUID aleatorio para el ID
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Token = "token123" // Genera un nuevo GUID aleatorio como token
            };

            // Devuelve el objeto AuthenticationResponse encapsulado en una tarea completada
            return Task.FromResult(response);
        }

        public Task<AuthenticationResponse> Register(AuthenticationRegisterAdapter request)
        {
            // Aquí delegamos el manejo de la solicitud al método Handle definido anteriormente
            return Handle(request, new CancellationToken());
        }
    }
}
