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
        private readonly IJwtDecodeToken _jwtDecodeToken;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion private fields

        public AuthenticationService(
            UserManager<IdentityUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator,
            IJwtDecodeToken jwtDecodeToken
        )
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _jwtDecodeToken = jwtDecodeToken;
            _userManager = userManager;
        }

        public async Task<AuthenticationResponse> Login(AuthenticationLoginAdapter loginRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Email);

            if (user is null)
            {
                throw new ArgumentException("Email or Password doest not exist");
            }

            var isCorrectThePassword = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (!isCorrectThePassword)
            {
                throw new ArgumentException("Email or password doest not exist");
            }

            var Identityuser = new IdentityUser { UserName = user.UserName, Email = user.Email };

            var loginInfo = new UserLoginInfo("ExcelToolsAPI", user.Id, user.UserName);

            var saveLogin = await _userManager.AddLoginAsync(Identityuser, loginInfo);

            // if (!saveLogin.Succeeded)
            // {
            //     throw new ArgumentException("Somethin was wrong saving the login");
            // }

            var userId = new Guid(user.Id);

            TokenRequest tokenRequest = new TokenRequest
            {
                UserId = userId,
                FirstName = user.UserName,
            };

            var token = _jwtTokenGenerator.GenerateToken(tokenRequest);

            var response = new AuthenticationResponse
            {
                Id = userId,
                FirstName = user.UserName,
                Email = user.Email,
                Token = token
            };

            return response;
        }

        public async Task<AuthenticationResponse> Register(AuthenticationRegisterAdapter request)
        {

            var user = new IdentityUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);


            var userId = new Guid(user.Id);
            TokenRequest tokenRequest = new TokenRequest
            {
                UserId = userId,
                FirstName = request.FirstName,
            };

            var token = _jwtTokenGenerator.GenerateToken(tokenRequest);


            var response = new AuthenticationResponse
            {
                Id = userId,
                FirstName = request.FirstName,
                Email = request.Email,
                Token = token
            };

            if (result.Succeeded)
            {
                return response;
            }
            else
            {
                throw new ArgumentException("Something was wrong register the user");
            }
        }

        public async Task<AuthenticationResponse> RenovateToken(AuthenticationTokenRequestAdapter request)
        {

            var data = _jwtDecodeToken.DecodeToken(request.Token);

            var user = await _userManager.FindByIdAsync(data.Id.ToString());

            if (user is null)
            {
                throw new ArgumentException("Invalid Token");
            }

            var userId = new Guid(user.Id);

            TokenRequest tokenRequest = new TokenRequest
            {
                UserId = userId,
                FirstName = user.UserName,
            };

            // creo que deberi de registrar un login en la base de datos
            var token = _jwtTokenGenerator.GenerateToken(tokenRequest);

            var response = new AuthenticationResponse
            {
                Id = userId, // Asigna el mismo userId generado previamente
                FirstName = user.UserName,
                Email = user.Email,
                Token = token
            };

            return response;
        }
    }
}
