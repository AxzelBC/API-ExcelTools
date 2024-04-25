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

        public Task<AuthenticationResponse> Handle(AuthenticationRegisterAdapter request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResponse> Login(LoginRequestDTO requet)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResponse> Register(AuthenticationRegisterAdapter request)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResponse> RenovateToken(TokenRequestDTO tokenRequestDTO)
        {
            throw new NotImplementedException();
        }


    }
}
