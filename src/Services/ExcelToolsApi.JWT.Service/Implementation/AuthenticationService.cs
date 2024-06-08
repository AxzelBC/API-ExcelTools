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

            // I want to save the login in the database
            var saveLogin = await _userManager.AddLoginAsync(Identityuser, loginInfo);

            // if (!saveLogin.Succeeded)
            // {
            //     throw new ArgumentException("Somethin was wrong saving the login");
            // }

            var userId = new Guid(user.Id);

            TokenRequest tokenRequest = new TokenRequest
            {
                UserId = userId,
            };

            var token = _jwtTokenGenerator.GenerateToken(tokenRequest);

            var response = new AuthenticationResponse
            {
                Id = userId,
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
            };

            var token = _jwtTokenGenerator.GenerateToken(tokenRequest);


            var response = new AuthenticationResponse
            {
                Id = userId,
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
            var user = await _userManager.FindByIdAsync(request.Id);

            if (user is null)
            {
                throw new ArgumentException("Invalid Token");
            }

            var userId = new Guid(user.Id);

            TokenRequest tokenRequest = new TokenRequest
            {
                UserId = userId,
            };

            var token = _jwtTokenGenerator.GenerateToken(tokenRequest);

            var response = new AuthenticationResponse
            {
                Id = userId,
                Token = token
            };

            return response;
        }
    }
}
