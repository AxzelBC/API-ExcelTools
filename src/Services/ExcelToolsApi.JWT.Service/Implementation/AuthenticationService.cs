using ExcelToolsApi.Domain.Request;
using ExcelToolsApi.Domain.Response;
using ExcelToolsApi.JWT.Service.Contract;
using Microsoft.AspNetCore.Identity;

namespace ExcelToolsApi.JWT.Service.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        #region private fields

        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion private fields

        public AuthenticationService(
            UserManager<IdentityUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator
        )
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
        }

        public async Task<AuthenticationResponse> Login(AuthenticationLoginAdapter loginRequestDTO)
        {
            // register
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Email);

            if (user is null)
            {
                throw new ArgumentException("The user does not exist");
            }
            var isCorrectThePasswors = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (!isCorrectThePasswors)
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

        public async Task<AuthenticationResponse> Register(AuthenticationRegisterAdapter request)
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

        public async Task<AuthenticationResponse> RenovateToken(AuthenticationTokenRequestAdapter request)
        {
            // deberia recibir un token
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

            // creo que deberi de registrar un login en la base de datos
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
}
