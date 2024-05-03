using ExcelToolsApi.Domain.Request;
using ExcelToolsApi.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using ExcelToolsApi.JWT.Service.Contract;
using ExcelToolsApi.JWT.Service;
using ExcelToolsApi.Domain.DTO;
using MediatR;

namespace ExcelTools.API.Controller
{
    [Route("api/auth")]
    [ApiController]
    public class AutheticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediatR;

        public AutheticationController(
            IAuthenticationService auth,
            IMediator mediatR
        )
        {
            _authenticationService = auth;
            _mediatR = mediatR;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterResquest request)
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
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            // adapter de authenticatioLoding Adapter a loginrequestDTO
            var adapter = new LoginRequestDTO
            {
                Email = request.Email,
                Password = request.Password
            };
            var response = _mediatR.Send(adapter);

            return Ok(response);
        }
        [HttpPost("renovateToken")]
        public async Task<IActionResult> RenovateToken(TokenRequestDTO request)
        {


            return Ok();
        }
    }
}
