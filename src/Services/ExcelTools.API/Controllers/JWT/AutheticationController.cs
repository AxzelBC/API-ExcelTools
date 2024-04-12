using ExcelToolsApi.Domain.Request; // Importar el espacio de nombres correcto
using ExcelToolsApi.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using ExcelToolsApi.JWT.Service.Contract;
using ExcelToolsApi.JWT.Service;
using ExcelToolsApi.Domain.DTO;

namespace ExcelTools.API.Controller
{
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
        public async Task<IActionResult> Register(RegisterResquest request) // Hacer el método asincrónico
        {
            var adapter = new AuthenticationRegisterAdapter
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password
            };

            var authResult = await _authenticationService.Register(adapter);

            var response = new AuthenticationResponse
            {
                Id = authResult.Id,
                FirstName = authResult.FirstName,
                LastName = authResult.LastName,
                Email = authResult.Email,
                Token = authResult.Token
            };

            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO request) // Hacer el método asincrónico
        {
            var adapter = new LoginRequestDTO
            {
                Email = request.Email,
                Password = request.Password
            };

            var authResult = await _authenticationService.Login(adapter);

            var response = new AuthenticationResponse
            {
                Id = authResult.Id,
                FirstName = authResult.FirstName,
                LastName = authResult.LastName,
                Email = authResult.Email,
                Token = authResult.Token
            };

            return Ok(response);
        }
    }
}
