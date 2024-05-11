using ExcelToolsApi.Domain.Request;
using Microsoft.AspNetCore.Mvc;
using ExcelToolsApi.JWT.Service;
using ExcelToolsApi.Domain.DTO;
using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ExcelTools.API.Controller
{
    [Route("api/auth")]
    [ApiController]
    public class AutheticationController : ControllerBase
    {
        #region private fields

        private readonly IMediator _mediatR;
        private readonly IMapper _mapper;
        #endregion private fields


        public AutheticationController(
            IMediator mediatR,
            IMapper mapper
        )
        {
            _mediatR = mediatR;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterResquest request)
        {
            var registerDTO = new RegisterRequestDTO
            {
                FirstName = request.FirstName,
                Email = request.Email,
                Password = request.Password
            };

            var adapter = _mapper.Map<AuthenticationRegisterAdapter>(registerDTO);

            var response = await _mediatR.Send(adapter);

            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            // adapter de authenticatioLoding Adapter a loginrequestDTO
            var loginDTO = new LoginRequestDTO
            {
                Email = request.Email,
                Password = request.Password
            };

            var adapter = _mapper.Map<AuthenticationLoginAdapter>(loginDTO);

            var response = await _mediatR.Send(adapter);

            return Ok(response);
        }

        [HttpGet("renovateToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RenovateToken()
        {
            var idClaim = HttpContext.User.Claims.Where(claim => claim.Type == "user_id").FirstOrDefault();
            if (idClaim == null)
            {
                throw new Exception("invalid token");
            }
            var id = idClaim.Value;

            var tokenDTO = new TokenRequestDTO
            {
                Id = id
            };

            var adapter = _mapper.Map<AuthenticationTokenRequestAdapter>(tokenDTO);

            var response = await _mediatR.Send(adapter);

            return Ok(response);
        }
    }
}
